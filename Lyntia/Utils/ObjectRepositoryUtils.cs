using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lyntia.Utils
{
	public class ObjectRepositoryUtils
	{
		Dictionary<String, List<String>> database = new Dictionary<String, List<String>>();

		/**
		 * Read all data of the csv and store as a database.
		 */
		public void testDataReader(String csvFile)
		{
			String line = "";
			String csvSplitBy = ";";

			using (var reader = new StreamReader(csvFile))
			{
				while (!reader.EndOfStream)
				{
					List<String> dataList = new List<string>();

					foreach (String value in line.Split(csvSplitBy)) 
					{
						dataList.Add(value);
					}

					database.TryAdd(dataList[0], dataList.GetRange(1, dataList.Count));
				}
			}
		}

		/**
		 * Take the data from csv file with objectName and the name of the column data.
		 * 
		 * @param objectName
		 *            The objectName of the test case that you need to search.
		 * @return The value of the column data in the line of the objectName in the csv.
		 */
		public String ObjectID(String objectName)
		{

			return database[objectName][1];

		}

		/**
		 * Take the data from csv file with objectName and the name of the column data.
		 * 
		 * @param objectName
		 *            The objectName of the test case that you need to search.
		 * @return The type value of the column data in the line of the objectName in the csv.
		 */
		public String TypeObjectID(String objectName)
		{
			return database[objectName][0];

		}
	}
}
