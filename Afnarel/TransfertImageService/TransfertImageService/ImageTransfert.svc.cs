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

    public class ImageTransfert : IImageTransfert
    {
        private AccesDonnees bdAccess = new AccesDonnees();

        public ImageUploadResponse UploadImage(ImageUploadRequest request) // , String name
        {
            Stream image = request.ImageData;
            ImageInfo infos = request.ImageInfo;

            // Stocker l’image en BDD
            byte[] imageBytes = null;
            MemoryStream imageStreamEnMemoire = new MemoryStream();
            image.CopyTo(imageStreamEnMemoire);
            imageBytes = imageStreamEnMemoire.ToArray();
            String imageID = bdAccess.uploadImage(imageBytes, infos.Name, infos.Album); // name
            imageStreamEnMemoire.Close();
            image.Close();

            infos.ID = imageID;

            ImageUploadResponse resp = new ImageUploadResponse();
            resp.ImageInfo = new ImageInfo();
            resp.ImageInfo.Album = infos.Album;
            resp.ImageInfo.Name = infos.Name;
            resp.ImageInfo.ID = infos.ID;
            return resp;
        }

        public Stream DownloadImage(String imageID)
        {
            // Récupérer l'image stockée en BDD et la transférer au client
            byte[] imageBytes = bdAccess.getImage(imageID);
            MemoryStream imageStreamEnMemoire = new MemoryStream(imageBytes);
            return imageStreamEnMemoire;
        }


        public int AddUser(string user, string password)
        {
            return bdAccess.addUser(user, password);
        }


        public bool DeleteUser(string user)
        {
            return bdAccess.deleteUser(user);
        }


        public bool DeleteImage(string hash)
        {
            return bdAccess.deleteImage(hash);
        }


        public int CreateAlbum(string name, string user)
        {
            return bdAccess.createAlbum(name, user);
        }


        public bool DeleteAlbum(int id)
        {
            return bdAccess.deleteAlbum(id);
        }
    }
}