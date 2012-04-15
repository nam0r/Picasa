using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;

namespace ImageTransfert.svc
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    public class ImageTransfert : IImageTransfert
    {

        // la classe AccesDonnees n’est pas donnée ici  
        //private AccesDonnees accesDonnees = new AccesDonnees();

        public String UploadImage(Stream image)
        {
            // Stocker l’image en BDD 
            byte[] imageBytes = null;
            MemoryStream imageStreamEnMemoire = new MemoryStream();
            image.CopyTo(imageStreamEnMemoire);
            imageBytes = imageStreamEnMemoire.ToArray();
            //String imageID = bdAccess.addImage(imageBytes);
            imageStreamEnMemoire.Close();
            image.Close();
            return "";// imageID;
        }


        public Stream DownloadImage(String imageID)
        {
            // Récupérer l'image stockée en BDD et la transférer au client 
            //byte[] imageBytes = bdAccess.getImage(imageID);
            MemoryStream imageStreamEnMemoire = new MemoryStream(imageBytes);
            return imageStreamEnMemoire;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
