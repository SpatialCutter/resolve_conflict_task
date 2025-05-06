using System;

namespace EcrypterApp
{
    //Класс шифра слово Цезаря, реализует шифруемый интерфейс
    //наследуется от шифра Цезаря
    internal class CesaroWordCypher : CesaroCypher, IEncryptable
    {
        //Метод шифрования
        public new string Encrypt(string text, int key)
        {
            //Разделить исходную строку на слова
            var words = text.Split(' ');
            //Для каждого слова
            for (int i = 0; i < words.Length; i++)
                //Зашифровать слово шифром Цезаря, с ключом
                //равным длине слова умноженной на введённый ключ
                words[i] = base.Encrypt(words[i], words[i].Length*key);
            //Объединить все слова и вернуть конечный результат
            return String.Join(" ", words);
        }
        
        //Метод дешифрования
        public new string Decrypt(string text, int key)
        {
            //Разделить исходную строку на слова
            var words = text.Split(' ');
            //Для каждого слова
            for (int i = 0; i < words.Length; i++)
                //Вызвать метод дешифрования класса шифра Цезаря, с ключом
                //равным длине слова умноженной на введённый ключ
                words[i] = base.Decrypt(words[i], words[i].Length * key);
            //Объединить все слова и вернуть конечный результат
            return String.Join(" ", words);
        }
    }
}
