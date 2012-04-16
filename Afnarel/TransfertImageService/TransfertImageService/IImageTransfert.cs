using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;

namespace TransfertImageService
{
    [ServiceContract]
    public interface IImageTransfert
    {

        /**
         * Upload l'image "image" dans la BDD avec comme id son hash et comme nom "name".
         * Retourne le hash de l'image si tout s'est bien passé, null sinon.
         */
        // String UploadImage(Stream image); // , String name
        [OperationContract]
        ImageUploadResponse UploadImage(ImageUploadRequest request); // , String name

        [OperationContract]
        Stream DownloadImage(String name);

        [OperationContract]
        bool DeleteImage(String hash);

        [OperationContract]
        int CreateAlbum(String name, String user);

        [OperationContract]
        bool DeleteAlbum(int id);

        /**
         * Ajoute l'utilisateur de login "user" avec le mot de passe "password"
         * Retourne l'ID de l'utilisateur ou -1 si il y a eu une erreur (par exemple
         * si utilisateur de même login existe déjà dans la base de donnée car il
         * y a une contrainte d'unicité sur la colonne "login").
         */
        [OperationContract]
        int AddUser(String user, String password);

        /**
         * Supprime l'utilisateur de login "user" (unique).
         * Retourne true en cas de succès ou false s'il y a eu une erreur
         * (par exemple, si aucun utilisateur de login "user" n'existe dans
         * la base de donnée).
         */
        [OperationContract]
        bool DeleteUser(String user);
        /*
        String createNewAlbum(String wantedName, String user);
        void deleteAlbum(String albumID);
        List<String> getUserAlbum(String user);
        List<String> getViewableAlbum(String user);
        String addPicture(String albumID, Stream picture);
        void deletePicture(String albumID, String pictureID);
         */

    }


    [MessageContract]
    public class ImageUploadRequest
    {
        [MessageHeader(MustUnderstand = true)]
        public ImageInfo ImageInfo;

        [MessageBodyMember(Order = 1)]
        public Stream ImageData;
    }

    [MessageContract]
    public class ImageUploadResponse
    {
        [MessageHeader(MustUnderstand = true)]
        public ImageInfo ImageInfo;
    }

    [MessageContract]
    public class ImageDownloadResponse
    {
        [MessageBodyMember(Order = 1)]
        public Stream ImageData;
    }

    [MessageContract]
    public class ImageDownloadRequest
    {
        [MessageBodyMember(Order = 1)]
        public ImageInfo ImageInfo;
    }

    [DataContract]
    public class ImageInfo
    {
        [DataMember(Order = 1, IsRequired = true)]
        public string ID { get; set; }

        [DataMember(Order = 2, IsRequired = true)]
        public int Album { get; set; }

        [DataMember(Order = 3, IsRequired = true)]
        public string Name { get; set; }
    }
}