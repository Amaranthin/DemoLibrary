﻿using System;
using System.Collections;
using System.IO;
using System.Text;

namespace DemoLibrary
{
    internal class Program
    {

        static ArrayList books = new ArrayList();
        static string antetka = "";

        static void Main(string[] args)
        {
            try
            {
                string filePath = "..\\..\\DataFiles\\books.txt";
                //Относителна адресация

                ReadBooksFromFile(filePath);

                // Пример за използване на данните
                foreach (Book book in books)
                {
                    Console.WriteLine($"Книга: {book.Title}, Автор: {book.Author}, Година: {book.Year}, Цена: {book.Price}");
                }

                string outputFilePath = "..\\..\\DataFiles\\outputFile.txt";
                WriteBooksToFile(outputFilePath);

                Console.WriteLine("Данните са записани успешно в новия файл.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файлът не е намерен.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Възникна грешка: {e.Message}");
            }

            Console.ReadLine();
        }

        static void ReadBooksFromFile(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding("utf-8")))
            {
                string line;

                int row = 0;

                while ((line = sr.ReadLine()) != null)
                {
                   
                    string[] bookData = line.Split(',');

                    if (bookData.Length == 4 && row !=0)
                    {
                        string title = bookData[0];
                        string author = bookData[1];
                        int year = int.Parse(bookData[2]);
                        decimal price = decimal.Parse(bookData[3]);

                        Book book = new Book(title, author, year, price);
                        books.Add(book);
                    }
                    else
                    {
                        if (row==0)
                        {
                            antetka = line;
                        }
 
                        if (bookData.Length != 4)
                        {
                            Console.WriteLine($"Некоректни данни в ред {row}");
                        }
                    }
                    row++;
                }
            }
        }

        static void WriteBooksToFile(string filePath)
        {
            
            using (StreamWriter sw = new StreamWriter(filePath, false,
                    Encoding.GetEncoding("utf-8")))
            {
                sw.WriteLine(antetka);

                foreach (Book book in books)
                {
                    string line = $"{book.Title},{book.Author},{book.Year},{book.Price}";
                    sw.WriteLine(line);
                }
            }
        }

        class Book
        {
            public string Title { get; }
            public string Author { get; }
            public int Year { get; }
            public decimal Price { get; }

            public Book(string title, string author, int year, decimal price)
            {
                Title = title;
                Author = author;
                Year = year;
                Price = price;
            }
        }
    }
}
