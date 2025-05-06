namespace EcrypterApp
{
    //Интерфейс, указывающий на то, что класс является шифрующим
    internal interface IEncryptable
    {
        //Метод шифрования
        string Encrypt(string text, int key);
        //Метод дешифрования
        string Decrypt(string text, int key);
    }
}
