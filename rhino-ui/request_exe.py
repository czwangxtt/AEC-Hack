import os
import requests
import json

def main():
    folder = "{}\\Documents\\AECedamy".format(os.environ["USERPROFILE"])
    with open(folder + "\\request_data.json","r") as json_file:
        data = json.load(json_file)
        
        
    # Send a POST request
    
    if os.path.exists(folder + "\\return_data.json"):
        os.remove(folder + "\\return_data.json")
     
    data_to_send = data.get("data", None)
    url = data.get("url", None)
    response = requests.post(url, data=data_to_send)
    

    with open(folder + "\\return_data.json","w") as json_file:
        json.dump(response.json(), json_file)

    print (response.json())
    
    
if __name__ == '__main__':
    main()