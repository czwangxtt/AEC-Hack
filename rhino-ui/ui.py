#! python 2
import Rhino
import rhinoscriptsyntax as rs
import scriptcontext as sc
import traceback

import System
import Rhino.UI
import Eto
import random
import os
FORM_KEY = 'AECademy_modeless_form'


def try_catch(func):
    def wrapper(*args, **kwargs):
        try:
            return func(*args, **kwargs)
        except Exception as e:
            rs.TextOut(traceback.format_exc())
            return None
        
    return wrapper


class AECedamyUI(Eto.Forms.Form):

    # Initializer
    @try_catch
    def __init__(self):
 
        
        # Basic form initialization
        self.initiate_form()
        # Create the form's controls
        self.build_form()




    # Basic form initialization
    def initiate_form(self):
        self.Title = 'AECademy Hub'
        self.Padding = Eto.Drawing.Padding(5)
        self.Resizable = True
        self.Maximizable = False
        self.Minimizable = False
        self.ShowInTaskbar = True
        self.MinimumSize = Eto.Drawing.Size(200, 150)
        self.resource_folder = "{}\\bin".format(os.path.dirname(os.path.realpath(__file__)))


        # FormClosed event handler
        self.Closed += self.form_close_clicked

    
    # Create all of the controls used by the form
    def build_form(self):
        # Create table layout
        layout = Eto.Forms.TableLayout()
        layout.Padding = Eto.Drawing.Padding(10)
        layout.Spacing = Eto.Drawing.Size(5, 5)


        layout.Rows.Add(self.build_logo())
        
        
        layout.Rows.Add(Eto.Forms.Label(Text = 'Search Database with your sketch!\nScan QR code below to begin sketching!' ))
        layout.Rows.Add(self.build_qr_code())
        layout.Rows.Add(Rhino.UI.Controls.Divider())
        layout.Rows.Add(self.create_user_buttons())
        
        
        
        # Set the content
        self.Content = layout

            
    def build_logo(self):
        self.logo = Eto.Forms.ImageView()
        logo_path = r"{}\\logo.png".format(self.resource_folder)
        temp_bitmap = Eto.Drawing.Bitmap(logo_path)
        self.logo.Image = temp_bitmap.WithSize(200,200)
        return self.logo

    def build_qr_code(self):
        self.qr_code = Eto.Forms.ImageView()
        qr_code_path = r"{}\\QR.png".format(self.resource_folder)
        temp_bitmap = Eto.Drawing.Bitmap(qr_code_path)
        self.qr_code.Image = temp_bitmap.WithSize(100,100)
        return self.qr_code

    def create_user_buttons(self):
        # Action button, func TBD
        bt_action = Eto.Forms.Button(Text = ' Import! ')
        bt_action.Click += self.action_bt_clicked
        
        bt_fetch = Eto.Forms.Button(Text = ' Get Update! ')
        bt_fetch.Click += self.fetch_bt_clicked

        layout = Eto.Forms.TableLayout(Spacing = Eto.Drawing.Size(5, 5))
        layout.Rows.Add(Eto.Forms.TableRow(None,bt_fetch, bt_action, None))
        return layout


    @try_catch
    def action_bt_clicked(self, sender, e):
        print ("importing")
        
    @try_catch
    def fetch_bt_clicked(self, sender, e):
        print ("fetching")
        
        
    @try_catch
    def form_close_clicked(self, sender, e):

        # Dispose of the form and remove it from the sticky dictionary
        if sc.sticky.has_key(FORM_KEY):
            form = sc.sticky[FORM_KEY]
            if form:
                form.Dispose()
                form = None
            sc.sticky.Remove(FORM_KEY)
            

    


@try_catch
def show_ui():

    # See if the form is already visible
    if sc.sticky.has_key(FORM_KEY):
        return
        

    # Create and show form
    form = AECedamyUI()   
    form.Owner = Rhino.UI.RhinoEtoApp.MainWindow
    form.Show()
    sc.sticky[FORM_KEY] = form





######################  main code below   #########
if __name__ == "__main__":

    show_ui()


