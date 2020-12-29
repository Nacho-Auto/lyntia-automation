using System;
using System.Collections.Generic;
using System.IO;

namespace Lyntia.Utilities
{
	public class TestDataUtils
	{
		private readonly static TestDataUtils _instance = new TestDataUtils();

		private TestDataUtils()
		{
		}

		public static TestDataUtils Instance
		{
			get
			{
				return _instance;
			}
		}

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
					line = reader.ReadLine();

					foreach (String value in line.Split(csvSplitBy))
					{
						dataList.Add(value);
					}

					database.TryAdd(dataList[0], dataList.GetRange(1, dataList.Count-1));
				}
			}
		}

		/**
		 * Take the data from csv file with testID and the name of the column data.
		 * @param testId The testID of the test case that you need to search.
		 * @param columnData The name of the column data that you need to take.
		 * @return The value of the column data in the line of the testID in the csv.
		 */
		public String getDataFromRepository(String testID, String columnData)
		{
			// Taking the index of the columnData in database.
			int indexColumnData = database["TestID"].FindIndex(x => x == columnData);
			return database[testID][indexColumnData];
		}

		/***
		 * Add a new column into the map and add the value.
		 * @param testID
		 * @param newCaseID
		 */
		public void addNewsID(String testID, String columnData, String data)
		{
			database["TestID"].Add(columnData);
			int indexColumnData = database["TestID"].FindIndex(x => x == columnData);
			database[testID][indexColumnData] = data;
		}
	}
}
