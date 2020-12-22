using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace Lyntia.Utils
{
	public class ExecutionTools
	{
		public static ExecutionTools exec;

		private ExecutionTools()
		{
		}

		public static void setInstance()
		{
			exec = new ExecutionTools();
		}

		private String scenarioID;
		private String scenario;
		private String status;
		private String captureFolderName;
		private String captureFolderScenarioName;
		//private SimpleDateFormat dateTimeInGMT = new SimpleDateFormat("yyyy-MM-dd-hh-mm-ss");
		//private Date startDate;
		//private Date endDate;
		private Dictionary<String, String> listURLs;
		private static TestDataUtils data = new TestDataUtils();
		private static ObjectRepositoryUtils object = new ObjectRepositoryUtils();
		private static ExceptionHandleUtils exceptionTxt = new ExceptionHandleUtils();
		private static ExceptionHandleUtils assertTxt = new ExceptionHandleUtils();
		private static IWebDriver driver;
		private static Wait<IWebDriver> wait = DriverManager.getWait();
		private static Actions actions;
		private static String driversFolder;

		public static List<int> checkPriority = new List<int>();
		public static List<Boolean> checkCurrentStatus = new List<Boolean>();
		public static List<String> checkStepName = new List<String>();
		public static int count;
		public String date;
		public static List<String> detailDataReport = new List<String>();
		public static int stepCount;
		public static Robot robot;

		public static String configURL;

		/***
		 * Get the driver.
		 * @return driver.
		 */
		public static IWebDriver getDriver()
		{
			return driver;
		}

		/***
		 * Get the url.
		 * @param urlName.
		 * @return url.
		 */
		public String getURL(String urlName)
		{
			return listURLs.get(urlName);
		}

		/***
		 * Get the data.
		 * @return data.
		 */
		public static TestDataUtils getData()
		{
			return data;
		}

		/***
		 * Get the wait.
		 * @return wait.
		 */
		public static Wait<IWebDriver> getWait()
		{
			return wait;
		}

		/***
		 * Get the object.
		 * @return object.
		 */
		public static ObjectRepositoryUtils getObject()
		{
			return object;
		}

		/***
		 * Get the exceptionTxt.
		 * @return exceptionTxt.
		 */
		public static ExceptionHandleUtils getExceptionTxt()
		{
			return exceptionTxt;
		}

		/***
		 * Get the assertTxt.
		 * @return assertTxt.
		 */
		public static ExceptionHandleUtils getAssertTxt()
		{
			return assertTxt;
		}

		/***
		 * 
		 * @return
		 */
		public SimpleDateFormat getDateTimeInGMT()
		{
			return dateTimeInGMT;
		}

		/***
		 * 
		 * @return
		 */
		public Date getStartDate()
		{
			return startDate;
		}

		/***
		 * 
		 * @return
		 */
		public Date getEndDate()
		{
			return endDate;
		}

		/***
		 * Get the capture folder name.
		 * @return the capture folder scenario name.
		 */
		public String getCaptureFolderName()
		{
			return captureFolderName;
		}

		/***
		 * Get the capture folder scenario name.
		 * @return the capture folder scenario name.
		 */
		public String getCaptureFolderScenarioName()
		{
			return captureFolderName + "/" + captureFolderScenarioName;
		}

		/***
		 * Get the Scenario.
		 * @return
		 */
		public String getScenario()
		{
			return scenario;
		}

		/***
		 * Get the testID.
		 * @return testID.
		 */
		public String getScenarioID()
		{
			return scenarioID;
		}

		/***
		 * Set the capture folder name.
		 * @param the capture folder name.
		 */
		public void setCaptureFolderName(String name)
		{
			captureFolderName = name;
		}

		/***
		 * Set the capture folder scenario name.
		 * @return the capture folder scenario name.
		 */
		public String setCaptureFolderScenarioName()
		{
			String captureFolderScenarioName = getCaptureFolderName() + scenarioID + "/";
			return captureFolderScenarioName;
		}


		/***
		 * Set the Scenario.
		 * @param scenario
		 */
		public void setScenario(String scenario)
		{
			this.scenario = scenario;
		}

		/***
		 * Set the TestID.
		 * @param testID
		 */
		public void setScenarioID(String testID)
		{
			scenarioID = testID;
		}


		/***
		 * This method prepare the driver and all the repositories of object and data that will be needed.
		 * @param OS OperatingSystemsEnum that we will use. (WINDOWS or MACINTOSH or IOS or ANDROID)
		 * @param Browser BrowserOrNativeEnum driver that we will use. (CHROME or FIREFOX or IE or EDGE or SAFARI or NATIVE or DEFAULT)
		 * @param capabilities List of capabilities that we will need in our driver. The format of this strings are "capability:valueOfCapability".
		 * @throws Exception
		 */
		public void setConfig(String TestCaseName, String TestCaseID, OperatingSystemsEnum OS, BrowserOrNativeEnum Browser, String...capabilities) throws Exception
		{
			dateTimeInGMT.setTimeZone(TimeZone.getTimeZone("GMT+2:00"));
		Calendar firstDate = Calendar.getInstance();
		startDate = firstDate.getTime();
		
		listURLs = new HashMap<String, String>();

		Properties p = new Properties();
		configURL = System.getProperty("user.dir") + "/src/test/resources/config.properties";
		p.load(new FileInputStream(configURL));
		
		for (String prop : p.stringPropertyNames()) {
			String[] arrOfStr = prop.split("\\.");
			if (arrOfStr[0].equals("url")) {
				listURLs.put(arrOfStr[1], p.getProperty("url." + arrOfStr[1]));
			}
}

robot = new Robot();

data.testDataReader(p.getProperty("path.testdata"));
object.testDataReader(p.getProperty("path.objectrepository"));
exceptionTxt.testDataReader(p.getProperty("path.exceptionhandler"));
setCaptureFolderName(p.getProperty("path.capturefolder"));
setDriversFolder(p.getProperty("path.drivers"));

setScenario(TestCaseName);
setScenarioID(TestCaseID);

captureFolderScenarioName = TestCaseName;

CommonFunctions.setInstance();
DriverManager.setInstance();
DriverManager.setUpDriver(OS, Browser, capabilities);
driver = DriverManager.getDriver();

setCaptureFolderScenarioName();
actions = new Actions(driver);
	}

	private void setDriversFolder(String driversFolderName)
{
	driversFolder = driversFolderName;
}

public void afterExecution() throws Exception
{
	Calendar lastDate = Calendar.getInstance();
	endDate = lastDate.getTime();
	Long startTime = startDate.getTime();
	Long endTime = endDate.getTime();
	Long diffTime = endTime - startTime;
	Long h = diffTime/(1000 * 60 * 60);
	Integer hours = h.intValue();
	Long m = diffTime/(1000 * 60);
	Integer minutes = m.intValue();
	Long s = diffTime/(1000);
	Integer seconds = s.intValue();
	date = hours.toString() + ":" + minutes.toString() + ":" + seconds.toString();
	doAfterLastStep(status);
	String statusTestCase = statusManager();
	System.out.println(
				"------------------------------------------------------------------------------------------------------------------------");
	System.out.println(scenario + " Status - " + statusTestCase);
	System.out.println(
				"------------------------------------------------------------------------------------------------------------------------");
	driver.quit();
	detailDataReport.add(statusTestCase);
	detailDataReport.add(getDateTimeInGMT().format(getStartDate()));
	detailDataReport.add(date);
	detailDataReport.add(executionStatus());
	detailDataReport.add(stepFailed());
	detailDataReport.add(verificationStatus());
	detailDataReport.add(verificationFailed());
	}

	/***
	 * This method manage the different status that the framework has.
	 * @return the status of the test. It will be BLOCKED, FAILED, PASSED WITH FAILURES or PASSED.
	 */
	public String statusManager()
{
	String internalStatus = "";
	for (int i = 0; i < checkPriority.size(); i++)
	{
		if (checkPriority.get(i).equals(1) && checkCurrentStatus.get(i).equals(false))
		{
			return "BLOCKED";
		}
		else if (checkPriority.get(i).equals(2) && checkCurrentStatus.get(i).equals(false))
		{
			return "FAILED";
		}
		else if (checkPriority.get(i).equals(3) && checkCurrentStatus.get(i).equals(false))
		{
			internalStatus = "PASSED WITH FAILURES";
		}
		else if (!internalStatus.equals("PASSED WITH FAILURES"))
		{
			internalStatus = "PASSED";
		}
	}
	return internalStatus;
}

/***
 * This method manage the status when a step fails.
 * @return status of the step.
 */
public String stepFailed()
{
	String internalStatus = "";
	for (int i = 0; i < checkPriority.size(); i++)
	{
		if ((checkPriority.get(i).equals(1) || checkPriority.get(i).equals(2)) && checkCurrentStatus.get(i).equals(false))
		{
			return checkStepName.get(i);
		}
		else if (checkPriority.get(i).equals(3) && checkCurrentStatus.get(i).equals(false))
		{
			return "¡¡verificaciones a colocar!!";
		}
		else if (!internalStatus.equals("PASSED WITH FAILURES"))
		{
			internalStatus = "";
		}
	}
	return internalStatus;
}

/***
 * This method manage the execution status.
 * @return execution status.
 */
public String executionStatus()
{
	String internalStatus = "";
	for (int i = 0; i < checkPriority.size(); i++)
	{
		if ((checkPriority.get(i).equals(1) || checkPriority.get(i).equals(2)) && checkCurrentStatus.get(i).equals(false))
		{
			return "FAILED";
		}
		else if (!(checkPriority.get(i).equals(1) || checkPriority.get(i).equals(2)))
		{
			internalStatus = "PASSED";
		}

	}
	return internalStatus;
}

/***
 * This method manage the verification status.
 * @return verification status.
 */
public String verificationStatus()
{
	String internalStatus = "";
	for (int i = 0; i < checkPriority.size(); i++)
	{
		if (checkPriority.get(i).equals(3) && checkCurrentStatus.get(i).equals(false))
		{
			return "FAILED";
		}
		else if (!internalStatus.equals("PASSED WITH FAILURES"))
		{
			internalStatus = "PASSED";
		}
	}
	return internalStatus;
}

/***
 * This method manage when a verification is failed.
 * @return verification status.
 */
public String verificationFailed()
{
	String internalStatus = "";
	for (int i = 0; i < checkPriority.size(); i++)
	{
		if (checkPriority.get(i).equals(3) && checkCurrentStatus.get(i).equals(false))
		{
			return "¡¡verificaciones a colocar!!";
		}
		else if (!internalStatus.equals("PASSED WITH FAILURES"))
		{
			internalStatus = "";
		}
	}
	return internalStatus;
}

/***
 * This method is used after the last step.
 * @param scenario. It is taken automatically from cucumber.
 */
public void doAfterLastStep(String status)
{
	checkCurrentStatus.remove(checkCurrentStatus.size() - 1);
	switch (status)
	{
		case "PASSED":
			checkCurrentStatus.add(true);
			checkStepName.add("");
			break;
		case "FAILED":
			checkCurrentStatus.add(false);
			checkStepName.add("");
			break;
	}
}

public static Actions getActions()
{
	return actions;
}

public static String getDriversFolder()
{
	return driversFolder;
}
	}
}
