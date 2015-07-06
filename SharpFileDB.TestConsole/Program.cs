﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharpFileDB.TestConsole
{
    class Program
    {

        static void Main(string[] args)
        {

            //DemoStringAndBytesLengthOfDocument.TypcialScene();
            //DemoTypeSerialization.TypicalScene();
            //DemoDocumentMeta.TypicalScene();
            //DemoSingleFileDB.TypicalScene2();
            //DemoSingleFileDB.TypicalScene();
            //DemoMaxFilesInDirectory.TypicalScene();
            //DemoGuid.TypicalScene();
            //DemoTcpMsg.TypicalScene();

            //IPersistence ipersistence = new DefaultPersistence();
            string fullname = Path.Combine(Environment.CurrentDirectory, "TestDatabase", "test.db");
            //string fullname = @"D:\360云盘\文档\TestDatabase\test.db";
            //string fullname = @"C:\Users\DELL\Documents\百度云同步盘\SharpFileDB\TestDatabase\test.db";

            // common cases to use SharpFileDB.
            FileDBContext db = new FileDBContext(fullname);

            Cat cat = new Cat();
            cat.KittyName = "kitty白3";
            cat.Price = 3;
            cat.FavoriteFood = new Food() { FoodName = "小黄鱼", };
            //cat.HeadPortrait = Image.FromFile(@"CatHeadPortrait.png");
            cat.OwnHobby = new Hobby() { Note = "this takes some time", SportHour = 2.5 };
            db.Insert(cat);

            //Predicate<Cat> pre = new Predicate<Cat>(x => x.Name == "xiao xiao bai");
            //IList<Cat> cats = db.Retrieve(pre);
            IList<Cat> cats = db.FindAll<Cat>();

            //cat.Name = "xiao bai";
            //db.Update(cat);

            //db.Delete(cat);
        }
    }
}
