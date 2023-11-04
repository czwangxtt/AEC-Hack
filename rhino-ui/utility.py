
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
    


    return {"sample": "text"}


def download_file(url):
    filename = os.path.join(get_local_folder(), 'temp.py')


    client = WebClient()
    url = "https://raw.githubusercontent.com/mcneel/rhinoscriptsyntax/rhino-6.x/Scripts/tests/AddPatchTests.py" 
     

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
    model_url = data.get("model_url", None)
    if not model_url:
        model_url = os.path.dirname(os.path.realpath(__file__)) + "\\test\\test_sampler.3dm"
        
    print (model_url)
    
    local_model_path = download_file(model_url)
    if not local_model_path:
        
        return
    
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

