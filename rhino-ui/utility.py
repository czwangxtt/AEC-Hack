import json
import os
# import requests
import rhinoscriptsyntax as rs
# import System
# from System import Net
# from System.IO import Path, File, FileStream, FileMode, FileAccess, FileShare
import clr
clr.AddReference('System.Net')
from System.Net import WebClient



def get_local_folder():
    folder = "{}\\Documents\\AECedamy".format(os.environ["USERPROFILE"])
    if not os.path.exists(folder):
        os.makedirs(folder)
    return folder

def fetch_from_web():
    # import requests
    # Replace with the URL you want to send a POST request to
    url = "https://aecademyhubserverserver20231104151838.azurewebsites.net/api/Suggestion/GetSuggestion"
    
    
    # Data you want to send, getting the first in the queue
    data_to_send = {"prompt": "string",
                    "base64ImageData": "string"}


    data = {"data":data_to_send,
            "url":url}
    with open(get_local_folder() + "\\request_data.json","w") as json_file:
        json.dump(data, json_file)

    safety = 0
    while True:
        print (safety)
        if os.path.exists(get_local_folder() + "\\return_data.json"):
            break
        rs.Sleep(1000)
        safety += 1
        if safety > 10:
            return {}
        
    with open(get_local_folder() + "\\return_data.json","r") as json_file:
        data = json.load(json_file)
        
    print (data)

    return data


def download_file(url):
    # if not a url, this is just a internal test location, just return the original address
    if not (url.startswith("http://") or url.startswith("https://")):
        return url
    
    
    file_raw = url.split('/')[-1]
    filename = os.path.join(get_local_folder(), file_raw)


    client = WebClient()
     

    try:
        client.DownloadFile(url, filename)
        print("Download successful, file saved as: " + filename)
        return filename
    except Exception as ex:
        print("Something went bad: " + str(ex))
        return None
    
    
    
    

    # Create a request for the URL. 
    request = Net.WebRequest.Create(url)

    # If required by the server, set the credentials.
    # request.Credentials = Net.CredentialCache.DefaultCredentials

    # Get the response.
    response = request.GetResponse()

    # Get the stream containing content returned by the server.
    dataStream = response.GetResponseStream()

    # Open the stream using a FileStream for writing.
    with FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None) as fs:
        # Read the bytes from the stream and write them to the file
        byteBuffer = System.Array.CreateInstance(System.Byte, 8192)
        bytesRead = dataStream.Read(byteBuffer, 0, byteBuffer.Length)
        while bytesRead > 0:
            fs.Write(byteBuffer, 0, bytesRead)
            bytesRead = dataStream.Read(byteBuffer, 0, byteBuffer.Length)

    # Clean up the streams and the response.
    dataStream.Close()
    response.Close()
    
    print (filename)
    return filename
    
    
    
    
    
    # Make the HTTP request to get the content of the file
    # response = requests.get(url)

    # Check if the request was successful
    if response.status_code == 200:

        # Write the content to a file
        with open(filename, 'wb') as f:
            f.write(response.content)
        print("Download complete! Check your folder for the file.")
        return filename
    else:
        print("Failed to retrieve the file. Status Code:", response.status_code)
        return None
        
def import_to_rhino(data):
    model_url = data.get("url", None)
    if not model_url:
        model_url = os.path.dirname(os.path.realpath(__file__)) + "\\test\\test_sampler.3dm"
        model_url = "https://raw.githubusercontent.com/mcneel/rhinoscriptsyntax/rhino-6.x/Scripts/tests/AddPatchTests.py" 
        model_url = os.path.dirname(os.path.realpath(__file__)) + "\\test\\test.gh"
        
    print (model_url)
    
    download_path = download_file(model_url)
    if not download_path:
        
        return
    
    print download_path
    extention = os.path.splitext(download_path)[1]
    print extention
    func_dict = {".3dm": process_3dm,
                 ".gh":process_gh}
    func = func_dict.get(extention, process_other)
    func(download_path)


def process_3dm(local_model_path):
    
    rs.EnableRedraw(False)
    rs.Command("_-import \"{}\" -enter -enter".format(local_model_path))
    
    imported_objs = rs.LastCreatedObjects()
    layers_used = set()
    
    for obj in imported_objs:
        layer = rs.ObjectLayer(obj)
        layers_used.add(layer)
    layers_used = list(layers_used)

    parent_layer_prefix = "AECademy"

    #only need to change layer
    change_objs_layer(imported_objs, parent_layer_prefix)
    safely_delete_used_layer(layers_used)
    
    rs.AddObjectsToGroup(imported_objs, rs.AddGroup())
    rs.UnselectAllObjects()
    rs.SelectObjects(imported_objs)
    rs.ZoomSelected()
    
    print ("done")
    rs.EnableRedraw()

    


def change_objs_layer(objs, parent_layer_prefix):
    for obj in objs:
        current_layer = rs.ObjectLayer(obj)
        current_layer_color = rs.LayerColor(current_layer)
        desired_layer = parent_layer_prefix + "::" + current_layer
        if not rs.IsLayer(desired_layer):
            rs.AddLayer(name = desired_layer, color=current_layer_color)
        rs.ObjectLayer(obj, desired_layer)



def safely_delete_used_layer(layers_to_remove):
    for layer in layers_to_remove:
        rs.DeleteLayer(layer)
    rs.Command("_NoEcho _Purge _Pause _Materials=_No _BlockDefinitions=_No _AnnotationStyles=_No _Groups=_No _HatchPatterns=_No _Layers=_Yes _Linetypes=_No _Textures=_No Environments=_No _Bitmaps=_No _Enter")

def process_gh(download_path):
    print ("processing GH")
    rs.Command("-Grasshopper Document Open \"{}\" Enter".format(download_path))

def process_other(download_path):
    os.startfile(download_path)