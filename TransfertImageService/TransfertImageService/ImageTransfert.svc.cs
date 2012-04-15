using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using DBAccess;

namespace TransfertImageService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "ImageTransfert" à la fois dans le code, le fichier svc et le fichier de configuration.
    public class ImageTransfert : IImageTransfert
    {
        private AccesDonnees bdAccess = new AccesDonnees();

        public String UploadImage(Stream image)
        {
            // Stocker l’image en BDD
            byte[] imageBytes = null;
            MemoryStream imageStreamEnMemoire = new MemoryStream();
            image.CopyTo(imageStreamEnMemoire);
            imageBytes = imageStreamEnMemoire.ToArray();
            String imageID = bdAccess.addImage(imageBytes, "trololo");
            imageStreamEnMemoire.Close();
            image.Close();
            return imageID;
        }

        public Stream DownloadImage(String imageID)
        {
            // Récupérer l'image stockée en BDD et la transférer au client
            byte[] imageBytes = bdAccess.getImage(imageID);
            MemoryStream imageStreamEnMemoire = new MemoryStream(imageBytes);
            return imageStreamEnMemoire;
        }
    }
}