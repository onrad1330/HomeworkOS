﻿//Zadání
//Projděte tento kód a popište případné problémy a jak byste je vyřešili.
//Převeďte tento kód na webovou službu(ASP.NET Core Web API) a upravte kód tak aby bylo možné:
//konvertovat mezi formáty XML a JSON
//snadno přidat nový formát(např.Protobuf)
//odeslat zdrojový a stáhnout výsledný soubor z API
//načíst a uložit data z/do libovolné cesty na disku(případně cloud-storage)
//načíst data z HTTP URL(nelze ukládat)
//odeslat výsledný soubor e-mailem(stačí pouze nástřel)
//Napište testy
//Refactorujte kód tak, jak by jste si představovali produkční aplikaci, která je vyvíjena a provozována vaším týmem.
//Homework.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Notino.Homework
{
	//Mělo by být jako nový soubor, ne v jednom souboru
	public class Document
	{
		public string Title { get; set; }
		public string Text { get; set; }
	}

	class Program
	{
		static void Main(string[] args)
		{
			//místo var bych určitě napsal string - je to čistčí a praktičtější
			var sourceFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Source Files\\Document1.xml");
			var targetFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Target Files\\Document1.json");


			string input = "";
			try
			{
				FileStream sourceStream = File.Open(sourceFileName, FileMode.Open);
				var reader = new StreamReader(sourceStream);
				input = reader.ReadToEnd();
			}
			catch (Exception ex)
			{
				//toto nedává smysl, pokud chceme chybu dál nechat "probublat", mělo by tu být pouze throw;
				//zároveň by celé zpracování chyb dávalo smysl přesunout někam do Middlewaru.
				//Chybí mi tu ale odezva uživateli a případné logování
				throw new Exception(ex.Message);
			}


			//proměnná input zde není definovaná, kvůli try-catch, je potřeba ji definovat mimo try-catch blok
			var xdoc = XDocument.Parse(input);
			var doc = new Document
			{
				Title = xdoc.Root.Element("title").Value,
				Text = xdoc.Root.Element("text").Value
			};

			var serializedDoc = JsonConvert.SerializeObject(doc);

			var targetStream = File.Open(targetFileName, FileMode.Create, FileAccess.Write);
			var sw = new StreamWriter(targetStream);
			sw.Write(serializedDoc);


		}
	}
}
