
import os
import rhinoscriptsyntax as rs


def fetch_from_web():
    
    
    
    
    return {"sample": "text"}



def import_to_rhino(data):
    model_url = data.get("model_url", None)
    if not model_url:
        model_url = os.path.dirname(os.path.realpath(__file__)) + "\\test\\test_sampler.3dm"
        
    print (model_url)

    rs.Command("_-import \"{}\" -enter -enter".format(model_url))
    
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
    
    print ("done")

    


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

