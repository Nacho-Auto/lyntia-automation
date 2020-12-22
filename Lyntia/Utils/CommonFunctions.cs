using System;
using System.Collections.Generic;
using System.Text;

namespace Lyntia.Utils
{
	class CommonFunctions
	{

		public static CommonFunctions common;

		public static void setInstance()
		{
			common = new CommonFunctions();
		}

		private static ExceptionHandleUtils exceptionTxt = ExecutionTools.getExceptionTxt();
		private static ExceptionHandleUtils assertTxt = ExecutionTools.getAssertTxt();
		private static Wait<RemoteWebDriver> wait = ExecutionTools.getWait();
		private TestDataUtils data = ExecutionTools.getData();
		private ExecutionTools exec = ExecutionTools.exec;
		private ObjectRepositoryUtils object = ExecutionTools.getObject();
	private Robot robot = ExecutionTools.robot;
		private Alert alert;
		private static Boolean isHighlightEnabled = false;

		private String dataRepositoryPrefix = "data.";

		/**
		 * INTERNAL: Get the downloadManager
		 * 
		 * @return the downloadManager
		 */
		public static DownloadManager getDownloadManager()
		{
			return downloadManager;
		}

		/**
		 * INTERNAL
		 * @param highlight
		 */
		public static void setHighlight(Boolean highlight)
		{
			isHighlightEnabled = highlight;
		}

		/**
		 * INTERNAL: Get the wait.
		 * 
		 * @return wait.
		 */
		public static Wait<RemoteWebDriver> getWait()
		{
			return wait;
		}

		// TODO ENCAPSULAR
		// Ready to review
		/**
		 * Check the attribute of an element. If we need to know if a list of elements
		 * are displayed, we need to put the attribute "enabled" and the value "true" or
		 * "false" in the case that we need to know if the element doesn't appear.
		 * 
		 * @param attribute
		 *            Attribute of the element.
		 * @param value
		 *            Value that we need to check. Could be in the data repository, put
		 *            dataRepositoryPrefix + columnName of the data.
		 * @param idElements
		 *            xpaths or ids of the element in the repository.
		 * @throws Exception
		 */
		public Boolean checkAttributeElement(String attribute, String value, String...idElements) throws Exception
		{
			String exceptionText = "checkAttributeElement";
		try {
				boolean trueEnabled = false;
				boolean textRepo = false;
				boolean isCritical = false;

				if (value.equals("true"))
				{
					trueEnabled = true;
				}
				else if (value.length() > dataRepositoryPrefix.length()
					  && value.subSequence(0, dataRepositoryPrefix.length()).equals(dataRepositoryPrefix))
				{
					textRepo = true;
				}

				if (attribute.equals("enabled"))
				{
					if (trueEnabled)
					{
						for (String idElement : idElements)
						{
							if (idElement.contains("//"))
							{
								if (!(findElementsByXpath(idElement).size() > 0))
								{
									exceptionText = "checkEnabledElementByXPath";
									if (isCritical)
									{
										throw new Exception(exceptionTxt.exceptionText(exceptionText));
									}
									Assert.fail(assertTxt.assertText(exceptionText));
									return false;
								}
							}
							else
							{
								exceptionText = "checkEnabledElementInRepo";
								if (!(findElementsInRepository(idElement).size() > 0))
								{
									Assert.fail(assertTxt.assertText(exceptionText));
									return false;
								}
							}
						}
					}
					else
					{
						for (String idElement : idElements)
						{
							if (idElement.contains("//"))
							{
								if (findElementsByXpath(idElement).size() > 0)
								{
									exceptionText = "checkDisabledElementByXPath";
									if (isCritical)
									{
										throw new Exception(exceptionTxt.exceptionText(exceptionText));
									}
									Assert.fail(assertTxt.assertText(exceptionText));
									return false;
								}
							}
							else
							{
								exceptionText = "checkDisabledElementInRepo";
								if (findElementsInRepository(idElement).size() > 0)
								{
									Assert.fail(assertTxt.assertText(exceptionText));
									return false;
								}
							}
						}
					}
				}
				else if (attribute.equals("text"))
				{
					for (String idElement : idElements)
					{
						if (idElement.contains("//"))
						{
							if (textRepo)
							{
								if (!(findElementsByXpath(idElement).size() > 0))
								{
									exceptionText = "findElementsByXpath";
									if (isCritical)
									{
										throw new Exception(exceptionTxt.exceptionText(exceptionText));
									}
									Assert.fail(assertTxt.assertText(exceptionText));
									return false;
								}
								else if (!findElementByXpath(idElement).getText().equals(data.getDataFromRepository(
									  exec.getScenarioID(), value.substring(dataRepositoryPrefix.length()))))
								{
									exceptionText = "textInRepoElementByXpath";
									if (isCritical)
									{
										throw new Exception(exceptionTxt.exceptionText(exceptionText));
									}
									Assert.fail(assertTxt.assertText(exceptionText));
									return false;
								}
							}
							else
							{
								if (!(findElementsByXpath(idElement).size() > 0))
								{
									exceptionText = "findElementsByXpath";
									if (isCritical)
									{
										throw new Exception(exceptionTxt.exceptionText(exceptionText));
									}
									Assert.fail(assertTxt.assertText(exceptionText));
									return false;
								}
								else if (!findElementByXpath(idElement).getText().equals(value))
								{
									exceptionText = "textElementByXpath";
									if (isCritical)
									{
										throw new Exception(exceptionTxt.exceptionText(exceptionText));
									}
									Assert.fail(assertTxt.assertText(exceptionText));
									return false;
								}
							}
						}
						else if (idElement.contains("alert.text"))
						{
							if (textRepo)
							{
								if (!(alert.getText().contains(data.getDataFromRepository(exec.getScenarioID(),
										value.substring(dataRepositoryPrefix.length())))))
								{
									exceptionText = "alertContainsTextInRepo";
									if (isCritical)
									{
										throw new Exception(exceptionTxt.exceptionText(exceptionText));
									}
									Assert.fail(assertTxt.assertText(exceptionText));
									return false;
								}
							}
							else
							{
								if (!(alert.getText().contains(value)))
								{
									exceptionText = "alertContainsText";
									if (isCritical)
									{
										throw new Exception(exceptionTxt.exceptionText(exceptionText));
									}
									Assert.fail(assertTxt.assertText(exceptionText));
									return false;
								}
							}
						}
						else
						{
							if (textRepo)
							{
								if (!(findElementsInRepository(idElement).size() > 0))
								{
									exceptionText = "findElementsInRepo";
									if (isCritical)
									{
										throw new Exception(exceptionTxt.exceptionText(exceptionText));
									}
									Assert.fail(assertTxt.assertText(exceptionText));
									return false;
								}
								else if (!findElementInRepository(idElement).getText().equals(data.getDataFromRepository(
									  exec.getScenarioID(), value.substring(dataRepositoryPrefix.length()))))
								{
									exceptionText = "textInRepoInElementInRepo";

									if (isCritical)
									{
										throw new Exception(exceptionTxt.exceptionText(exceptionText));
									}
									Assert.fail(assertTxt.assertText(exceptionText));
									return false;
								}
							}
							else
							{
								if (!(findElementsInRepository(idElement).size() > 0))
								{
									exceptionText = "findElementsInRepo";

									if (isCritical)
									{
										throw new Exception(exceptionTxt.exceptionText(exceptionText));
									}
									Assert.fail(assertTxt.assertText(exceptionText));
									return false;
								}
								else if (!findElementInRepository(idElement).getText().equals(value))
								{
									exceptionText = "textElementInRepo";

									if (isCritical)
									{
										throw new Exception(exceptionTxt.exceptionText(exceptionText));
									}
									Assert.fail(assertTxt.assertText(exceptionText));
									return false;
								}
							}
						}
					}
				}
				else
				{
					for (String idElement : idElements)
					{
						if (idElement.contains("//"))
						{
							if (textRepo)
							{
								if (!(findElementsByXpath(idElement).size() > 0))
								{
									exceptionText = "findElementsByXpath";

									if (isCritical)
									{
										throw new Exception(exceptionTxt.exceptionText(exceptionText));
									}
									Assert.fail(assertTxt.assertText(exceptionText));
									return false;
								}
								else if (!findElementByXpath(idElement).getAttribute(attribute).equals(data
									  .getDataFromRepository(exec.getScenarioID(),
											  value.substring(dataRepositoryPrefix.length()))))
								{
									exceptionText = "attributeValueInRepoElementByXpath";

									if (isCritical)
									{
										throw new Exception(exceptionTxt.exceptionText(exceptionText));
									}
									Assert.fail(assertTxt.assertText(exceptionText));
									return false;
								}
							}
							else
							{
								if (!(findElementsByXpath(idElement).size() > 0))
								{
									exceptionText = "findElementsByXpath";

									if (isCritical)
									{
										throw new Exception(exceptionTxt.exceptionText(exceptionText));
									}
									Assert.fail(assertTxt.assertText(exceptionText));
									return false;
								}
								else if (!findElementByXpath(idElement).getAttribute(attribute).equals(value))
								{
									exceptionText = "attributeValueElementByXpath";

									if (isCritical)
									{
										throw new Exception(exceptionTxt.exceptionText(exceptionText));
									}
									Assert.fail(assertTxt.assertText(exceptionText));
									return false;
								}
							}
						}
					}
				}
				return true;

			} catch (Exception e) {
			common.quitDriver();
			throw new Exception(exceptionTxt.exceptionText(exceptionText));
	}
}

// DONE
/**
 * Clear all the text in the element.
 * 
 * @param idElement
 *            xpath or id of the element in the repository.
 * @throws Exception
 */
public void clearElement(String idElement) throws Exception
{
		try {
		if (idElement.contains("//"))
		{
			if (findElementsByXpath(idElement).size() > 0)
			{
				findElementByXpath(idElement).clear();
			}
			else
			{
				Assert.fail(assertTxt.assertText("clearElementByXpath"));
			}
		}
		else
		{
			if (findElementsInRepository(idElement).size() > 0)
			{
				findElementInRepository(idElement).clear();
			}
			else
			{
				Assert.fail(assertTxt.assertText("clearElementInRepo"));
			}
		}
	} catch (Exception e) {
		common.quitDriver();
		throw new Exception(exceptionTxt.exceptionText("clearElement"));
	}
}

// Ready to review
/**
 * Write, accept or dismiss alerts.
 * 
 * @param accept
 *            accept or dismiss the alert.
 * @param fieldsToFill
 *            Text that we want to write into the inputs of the alert
 *            (optional).
 * @throws Exception
 */
public void alertManager(Boolean accept, String...fieldsToFill) throws Exception
{
		try {
		alert = ExecutionTools.getDriver().switchTo().alert();
		if (accept)
		{
			if (fieldsToFill.length > 0)
			{
				String keysToSend = "";
				for (String fieldToFill : fieldsToFill)
				{
					keysToSend += fieldToFill + Keys.TAB;
				}
				alert.sendKeys(keysToSend);
			}
			alert.accept();
		}
		else
		{
			if (fieldsToFill.length > 0)
			{
				String keysToSend = "";
				for (String fieldToFill : fieldsToFill)
				{
					keysToSend += fieldToFill + Keys.TAB;
				}
				alert.sendKeys(keysToSend);
			}
			alert.dismiss();
		}
	} catch (Exception e) {
		common.quitDriver();
		throw new Exception(exceptionTxt.exceptionText("alertManager"));
	}
}

// DONE
/**
 * Click into the element put in the parameter.
 * 
 * @param idElement
 *            xpath or id of the element in the repository.
 * @throws Exception
 */
public void clickElement(String idElement) throws Exception
{
		try {
		if (idElement.contains("//"))
		{
			if (findElementsByXpath(idElement).size() > 0)
			{
				findElementByXpath(idElement).click();
			}
			else
			{
				Assert.fail(assertTxt.assertText("clickElementByXpath"));
			}
		}
		else
		{
			if (findElementsInRepository(idElement).size() > 0)
			{
				findElementInRepository(idElement).click();
			}
			else
			{
				Assert.fail(assertTxt.assertText("clickElementInRepo"));
			}
		}
	} catch (Exception e) {
		common.quitDriver();
		throw new Exception(exceptionTxt.exceptionText("clickElement"));
	}
}

// TODO Low priority
/**
 * INTERNAL: This method find a Screen element with an image.
 * 
 * @param nameImage
 * @return
 * @throws Exception
 */
public Screen findElementByImage(String nameImage) throws Exception
{
		try {
		Pattern prueba = new Pattern(nameImage);
		Screen s = new Screen(0);
		s.find(prueba);
		return s;
	} catch (Exception e) {
		common.quitDriver();
		throw new Exception(exceptionTxt.exceptionText("findElementByImage") + " ");
	}
}

// DONE
/**
 * INTERNAL: Find element by xPath.
 * 
 * @param xpath
 * @return WebElement
 * @throws Exception
 *             Predefine text is displayed in console when the Exception is
 *             throw.
 */
public WebElement findElementByXpath(String xpath) throws Exception
{
		try {
		highlighterElement(ExecutionTools.getDriver().findElement(By.xpath(xpath)));
		return ExecutionTools.getDriver().findElement(By.xpath(xpath));
	} catch (Exception e) {
		common.quitDriver();
		throw new Exception(exceptionTxt.exceptionText("findElementByXpath") + " " + xpath);
	}
}

// DONE
/**
 * INTERNAL: Find an unique WebElement object.
 * 
 * @param idInRepositoryElement
 * @return WebElement
 * @throws Exception
 *             Predefine text is displayed in console when the Exception is
 *             throw.
 */
public WebElement findElementInRepository(String idInRepositoryElement) throws Exception
{
		try {
		switch (object.TypeObjectID(idInRepositoryElement).toLowerCase())
		{
			case "xpath":
				if (ExecutionTools.getDriver().findElements(By.xpath(object.ObjectID(idInRepositoryElement)))
						.size() > 0)
				{
					highlighterElement(
							ExecutionTools.getDriver().findElement(By.xpath(object.ObjectID(idInRepositoryElement))));
					return ExecutionTools.getDriver().findElement(By.xpath(object.ObjectID(idInRepositoryElement)));
				}
			case "id":
				if (ExecutionTools.getDriver().findElements(By.id(object.ObjectID(idInRepositoryElement))).size() > 0)
				{
					highlighterElement(
							ExecutionTools.getDriver().findElement(By.id(object.ObjectID(idInRepositoryElement))));
					return ExecutionTools.getDriver().findElement(By.id(object.ObjectID(idInRepositoryElement)));
				}
			case "css":
				if (ExecutionTools.getDriver().findElements(By.className(object.ObjectID(idInRepositoryElement)))
						.size() > 0)
				{
					highlighterElement(ExecutionTools.getDriver()
							.findElement(By.className(object.ObjectID(idInRepositoryElement))));
					return ExecutionTools.getDriver().findElement(By.className(object.ObjectID(idInRepositoryElement)));
				}
		}
	} catch (Exception e) {
		common.quitDriver();
		throw new Exception(exceptionTxt.exceptionText("findElementInRepository") + " " + idInRepositoryElement);
	}
		return null;
}

// DONE
/**
 * INTERNAL: Find a list of elements by xPath.
 * 
 * @param idElement
 * @return List<WebElement>
 * @throws Exception
 *             Predefine text is displayed in console when the Exception is
 *             throw.
 */
public List<WebElement> findElementsByXpath(String xpath) throws Exception
{
		try {
		return ExecutionTools.getDriver().findElements(By.xpath(xpath));
	} catch (Exception e) {
		common.quitDriver();
		throw new Exception(exceptionTxt.exceptionText("findElementsByXpath") + " " + xpath);
	}
}

// Ready to review
/**
 * INTERNAL: Find a list of WebElements.
 * 
 * @param idInRepositoryElement
 * @return List<WebElement>
 * @throws Exception
 *             Predefine text is displayed in console when the Exception is
 *             throw.
 */
public List<WebElement> findElementsInRepository(String idInRepositoryElement) throws Exception
{
		try {
		switch (object.TypeObjectID(idInRepositoryElement).toLowerCase())
		{
			case "xpath":
				if (ExecutionTools.getDriver().findElements(By.xpath(object.ObjectID(idInRepositoryElement)))
						.size() > 0)
				{
					return ExecutionTools.getDriver().findElements(By.xpath(object.ObjectID(idInRepositoryElement)));
				}
			case "id":
				if (ExecutionTools.getDriver().findElements(By.id(object.ObjectID(idInRepositoryElement))).size() > 0)
				{
					return ExecutionTools.getDriver().findElements(By.id(object.ObjectID(idInRepositoryElement)));
				}
			case "css":
				if (ExecutionTools.getDriver().findElements(By.className(object.ObjectID(idInRepositoryElement)))
						.size() > 0)
				{
					return ExecutionTools.getDriver()
							.findElements(By.className(object.ObjectID(idInRepositoryElement)));
				}
		}
	} catch (Exception e) {
		common.quitDriver();
		throw new Exception(exceptionTxt.exceptionText("findElements") + " " + idInRepositoryElement);
	}
		return null;
}

/**
 * INTERNAL: Get the TestDataUtils.
 * 
 * @return TestDataUtils.
 */
public TestDataUtils getData()
{
	return data;
}

/**
 * INTERNAL: Get the driver.
 * 
 * @return driver.
 */
public RemoteWebDriver getDriver()
{
	return ExecutionTools.getDriver();
}

/**
 * INTERNAL: Get the ExceptionHandleUtils.
 * 
 * @return ExceptionHandleUtils.
 */
public ExceptionHandleUtils getExceptionTxt()
{
	return exceptionTxt;
}

/**
 * INTERNAL: Get the ObjectInRepositoryUtils.
 * 
 * @return ObjectInRepositoryUtils.
 */
public ObjectRepositoryUtils getObjectFromRepository()
{
	return object;
}

/**
 * INTERNAL: Get the Robot.
 * 
 * @return robot.
 */
public Robot getRobot()
{
	return robot;
}

// Ready to review
/**
 * 
 * @param idElement
 * @param attribute
 * @param columnName
 * @throws Exception
 */
public String getAndStoreAttributeOfElementInRepository(String idElement, String attribute, String columnName)
			throws Exception
{
		try {
		String valueOfAttribute = "";

		if (attribute == "text")
		{
			if (idElement.contains("//"))
			{
				if (findElementsByXpath(idElement).size() > 0)
				{
					valueOfAttribute = findElementByXpath(idElement).getText();
				}
				else
				{
					Assert.fail(assertTxt.assertText("findElementsByXpath"));
				}
			}
			else
			{
				if (findElementsInRepository(idElement).size() > 0)
				{
					valueOfAttribute = findElementInRepository(idElement).getText();
				}
				else
				{
					Assert.fail(assertTxt.assertText("findElementsInRepo"));
				}
			}
		}
		else
		{
			if (idElement.contains("//"))
			{
				if (findElementsByXpath(idElement).size() > 0)
				{
					valueOfAttribute = findElementByXpath(idElement).getAttribute(attribute);
				}
				else
				{
					Assert.fail(assertTxt.assertText("findElementsByXpath"));
				}
			}
			else
			{
				if (findElementsInRepository(idElement).size() > 0)
				{
					valueOfAttribute = findElementInRepository(idElement).getAttribute(attribute);
				}
				else
				{
					Assert.fail(assertTxt.assertText("findElementsInRepo"));
				}
			}
		}

		data.addNewsID(exec.getScenarioID(), columnName, valueOfAttribute);
		return valueOfAttribute;

	} catch (Exception e) {
		common.quitDriver();
		throw new Exception(exceptionTxt.exceptionText("getAndStoreAttributeOfElementInRepository"));
	}

}

// DONE
/**
 * INTERNAL: Function for highlight web elements.
 * 
 * @param webElement
 * @throws Exception
 */
public void highlighterElement(WebElement webElement) throws Exception
{
		if (isHighlightEnabled) {
		JavascriptExecutor js = (JavascriptExecutor)ExecutionTools.getDriver();
		for (int i = 0; i < 2; i++)
		{
			ExecutionTools.getActions().moveToElement(webElement);
			ExecutionTools.getActions().perform();
			waitForMiliseconds("100");
			js.executeScript("arguments[0].setAttribute('style', arguments[1]);", webElement,
					"color: black; border: 2px solid black;");
			waitForMiliseconds("200");
			js.executeScript("arguments[0].setAttribute('style', arguments[1]);", webElement, "");
			waitForMiliseconds("100");
		}
	}
}

// TODO Low priority
/**
 * INTERNAL:
 * 
 * @param idElement
 */
public void moveMouseToElementInRepository(String idElement)
{

}

// DONE
/**
 * Navigate to an URL.
 * 
 * @param page
 *            URL in config.properties or not.
 * @throws Exception
 */
public void navigateToPage(String page) throws Exception
{
		try {
		if (page.contains("//"))
		{
			ExecutionTools.getDriver().get(page);
		}
		else
		{
			ExecutionTools.getDriver().get(exec.getURL(page));
		}
	} catch (Exception e) {
		common.quitDriver();
		throw new Exception(exceptionTxt.exceptionText("navigateToPage"));
	}
}

// DONE
/**
 * Press and release any key.
 * 
 * @param key
 * @throws Exception
 */
public void pressKey(String key) throws Exception
{
		int keyCode = 0;
		try {
		switch (key)
		{
			case "ENTER":
				keyCode = KeyEvent.VK_ENTER;
				break;
			case "TAB":
				keyCode = KeyEvent.VK_TAB;
				break;
			case "DOWN":
				keyCode = KeyEvent.VK_DOWN;
				break;
			case "UP":
				keyCode = KeyEvent.VK_UP;
				break;
			case "RIGHT":
				keyCode = KeyEvent.VK_RIGHT;
				break;
			case "LEFT":
				keyCode = KeyEvent.VK_LEFT;
				break;
			default:
				keyCode = KeyEvent.getExtendedKeyCodeForChar(key.charAt(0));
				break;
		}
		robot.keyPress(keyCode);
		robot.keyRelease(keyCode);
	} catch (Exception e) {
		common.quitDriver();
		throw new Exception(exceptionTxt.exceptionText("pressKey"));
	}
}

// Ready to review
/**
 * INTERNAL: NATIVE FUNCTION. Click into a established coordinates.
 * 
 * @param x
 * @param y
 * @throws Exception
 *             Predefine text is displayed in console when the Exception is
 *             throw.
 */
public void pushIntoCoordinates(Double x, Double y) throws Exception
{
		try {
		if (ExecutionTools.getDriver() instanceof AppiumDriver<?>)
			{
				String currentContext = ((AppiumDriver <?>) ExecutionTools.getDriver()).getContext();
		((AppiumDriver <?>) ExecutionTools.getDriver()).context("NATIVE_APP");
		@SuppressWarnings("rawtypes")
				TouchAction touchAction = new TouchAction(((AppiumDriver <?>) ExecutionTools.getDriver()));
		double heightAllowButton = ((ExecutionTools.getDriver().manage().window().getSize().height) * x);
		double widthAllowButton = ((ExecutionTools.getDriver().manage().window().getSize().width) * y);
		touchAction.tap(PointOption.point((int)widthAllowButton, (int)heightAllowButton)).perform();
		((AppiumDriver <?>) ExecutionTools.getDriver()).context(currentContext);
	}
} catch (Exception e)
{
	common.quitDriver();
	throw new Exception(
			exceptionTxt.exceptionText("pushIntoCoordinates") + " " + x.toString() + " " + y.toString());
}
	}

	// DONE
	/**
	 * INTERNAL: Quit the driver.
	 * 
	 * @throws Exception
	 */
	public void quitDriver() throws Exception
{
		try {
		ExecutionTools.getDriver().quit();
	} catch (Exception e) {
		common.quitDriver();
		throw new Exception(exceptionTxt.exceptionText("quitDriver"));
	}
}

// Ready to review
/**
 * 
 * @param captureName
 * @throws Exception
 */
public void screenCapture(String captureName) throws Exception
{
	File src = ExecutionTools.getDriver().getScreenshotAs(OutputType.FILE);
		try {
		SimpleDateFormat dateTimeInGMT = new SimpleDateFormat("yyyy-MM-dd_HH-mm-ss");
		dateTimeInGMT.setTimeZone(TimeZone.getTimeZone("GMT+1:00"));
		FileUtils.copyFile(src, new File(exec.getCaptureFolderScenarioName() + "/"
				+ dateTimeInGMT.format(new Date()) + "/" + captureName + ".png"));
	} catch (IOException e) {
		common.quitDriver();
		System.out.println("|ERROR| " + e.getLocalizedMessage());
	}
}

// TODO Hace falta implementar el enviar una clave a un elemento.
/**
 * Send the repository or not repository text to the repository or not
 * repository element.
 * 
 * @param stringOrKeys
 *            Keys, String or dataRepositoryPrefix + id of the text in
 *            repository.
 * @param idElement
 *            xpath or id of the element in the repository.
 * @throws Exception
 */
public void sendKeysToElement(CharSequence stringOrKeys, String idElement) throws Exception
{
		try {
		boolean textRepo = false;

		if (stringOrKeys.length() > dataRepositoryPrefix.length()
				&& stringOrKeys.subSequence(0, dataRepositoryPrefix.length()).equals(dataRepositoryPrefix))
		{
			textRepo = true;
		}

		if (idElement.contains("//"))
		{
			if (findElementsByXpath(idElement).size() > 0)
			{
				if (textRepo)
				{
					findElementByXpath(idElement)
							.sendKeys(data.getDataFromRepository(exec.getScenarioID(), stringOrKeys
									.subSequence(dataRepositoryPrefix.length(), stringOrKeys.length()).toString()));
				}
				else
				{
					findElementByXpath(idElement).sendKeys(stringOrKeys);
				}
			}
			else
			{
				Assert.fail(assertTxt.assertText("findElementsByXpath"));
			}
		}
		else
		{
			if (findElementsInRepository(idElement).size() > 0)
			{
				if (textRepo)
				{
					findElementInRepository(idElement)
							.sendKeys(data.getDataFromRepository(exec.getScenarioID(), stringOrKeys
									.subSequence(dataRepositoryPrefix.length(), stringOrKeys.length()).toString()));
				}
				else
				{
					findElementInRepository(idElement).sendKeys(stringOrKeys);
				}

			}
			else
			{
				Assert.fail(assertTxt.assertText("findElementsInRepo"));
			}
		}
	} catch (Exception e) {
		common.quitDriver();
		throw new Exception(exceptionTxt.exceptionText("sendKeysToElement"));
	}
}

// DONE
/**
 * INTERNAL: Print in console a message.
 * 
 * @param message
 *            Message that is displayed in console.
 * @throws Exception
 *             Predefine text is displayed in console when the Exception is
 *             throw.
 */
public void showInConsole(String message) throws Exception
{
		try {
		System.out.println(message);
	} catch (Exception e) {
		common.quitDriver();
		throw new Exception(exceptionTxt.exceptionText("showInConsole"));
	}
}

// Ready to review
/**
 * 
 * @param whereSwitchTo
 * @param id
 * @throws Exception
 */
public void switchToWindowOrIFrame(String whereSwitchTo, String id) throws Exception
{
		try {
		switch (whereSwitchTo)
		{
			case "alert":
				alert = ExecutionTools.getDriver().switchTo().alert();
				break;
			case "frameByIndex":
				ExecutionTools.getDriver().switchTo().frame(Integer.parseInt(id));
				break;
			case "frameByNameOrId":
				if (id.contains(dataRepositoryPrefix))
				{
					ExecutionTools.getDriver().switchTo().frame(data.getDataFromRepository(exec.getScenarioID(),
							id.substring(dataRepositoryPrefix.length())));
				}
				else
				{
					ExecutionTools.getDriver().switchTo().frame(id);
				}
				break;
			case "frameByFrameElement":
				if (id.contains("//"))
				{
					if (findElementsByXpath(id).size() > 0)
					{
						ExecutionTools.getDriver().switchTo().frame(findElementByXpath(id));
					}
					else
					{
						Assert.fail(assertTxt.assertText("frameByXpath"));
					}
				}
				else
				{
					if (findElementsInRepository(id).size() > 0)
					{
						ExecutionTools.getDriver().switchTo().frame(findElementInRepository(id));
					}
					else
					{
						Assert.fail(assertTxt.assertText("frameInRepo"));
					}
				}
				break;
			case "windowByIndex":
				Set<String> windows = ExecutionTools.getDriver().getWindowHandles();

				Integer integerID = Integer.parseInt(id);
				Integer dimArray = integerID + 1;

				String[] windows_id = new String[dimArray];
				windows.toArray(windows_id);
				common.getDriver().switchTo().window(windows_id[integerID]);

				break;
			case "defaultContent":
				ExecutionTools.getDriver().switchTo().defaultContent();
				break;
			case "parentFrame":
				ExecutionTools.getDriver().switchTo().parentFrame();
				break;
		}
	} catch (Exception e) {
		common.quitDriver();
		throw new Exception(exceptionTxt.exceptionText("switchToWindowOrIFrame"));
	}
}

// DONE
/**
 * Explicit wait some miliseconds.
 * 
 * @param miliSeconds
 * @throws Exception
 */
public void waitForMiliseconds(String miliSeconds) throws Exception
{
		try {
		Thread.sleep(Long.parseLong(miliSeconds));
	} catch (Exception e) {
		common.quitDriver();
		throw new Exception(exceptionTxt.exceptionText("waitForSeconds"));
	}
}

// Ready to Review
/**
 * Check if the expected condition is true.
 * 
 * @param idExpectedCondition
 *            Could be one of the followings:
 *            <ul>
 *            <li>alertIsPresent</li>
 *            <li>attributeContains</li>
 *            <li>attributeToBe</li>
 *            <li>attributeToBeNotEmpty</li>
 *            <li>elementSelectionStateToBe</li>
 *            <li>elementToBeClickable</li>
 *            <li>elementToBeSelected</li>
 *            <li>frameToBeAvailableAndSwitchToIt</li>
 *            <li>invisibilityOf</li>
 *            <li>invisibilityOfAllElements</li>
 *            <li>textToBePresentInElement</li>
 *            <li>textToBePresentInElementValue</li>
 *            <li>titleContains</li>
 *            <li>titleIs</li>
 *            <li>urlContains</li>
 *            <li>urlToBe</li>
 *            <li>visibilityOf</li>
 *            <li>visibilityOfAllElements</li>
 *            </ul>
 * @param listOfNecessaryParams
 *            It depends to idExpectedCondition: *
 *            <ul>
 *            <li>alertIsPresent -> empty</li>
 *            <li>attributeContains -> idElement, attribute, value</li>
 *            <li>attributeToBe -> idElement, attribute, value</li>
 *            <li>attributeToBeNotEmpty -> idElement, attribute</li>
 *            <li>elementSelectionStateToBe -> idElement, isSelected</li>
 *            <li>elementToBeClickable -> idElement</li>
 *            <li>elementToBeSelected -> idElement</li>
 *            <li>frameToBeAvailableAndSwitchToIt -> idElement</li>
 *            <li>invisibilityOf -> idElement</li>
 *            <li>invisibilityOfAllElements -> idElements</li>
 *            <li>textToBePresentInElement -> idElement, text</li>
 *            <li>textToBePresentInElementValue -> idElement,
 *            value</li>
 *            <li>titleContains -> idTitle</li>
 *            <li>titleIs -> idTitle</li>
 *            <li>urlContains -> idUrl</li>
 *            <li>urlToBe -> idUrl</li>
 *            <li>visibilityOf -> idElement</li>
 *            <li>visibilityOfAllElements -> idElements</li>
 *            </ul>
 * @throws Exception
 */
public void waitToExpectedConditions(String idExpectedCondition, String...listOfNecessaryParams) throws Exception
{
		try {
		WebDriverWait wait = new WebDriverWait(common.getDriver(), 20);

		switch (idExpectedCondition)
		{
			case "alertIsPresent":
				wait.until(ExpectedConditions.alertIsPresent());
				break;
			case "attributeContains":
				if (listOfNecessaryParams[0].contains("//"))
				{
					if (listOfNecessaryParams[2].substring(0, dataRepositoryPrefix.length()) == dataRepositoryPrefix)
					{
						wait.until(ExpectedConditions.attributeContains(findElementByXpath(listOfNecessaryParams[0]),
								listOfNecessaryParams[1], data.getDataFromRepository(exec.getScenarioID(),
										listOfNecessaryParams[2].substring(dataRepositoryPrefix.length()))));
					}
					else
					{
						wait.until(ExpectedConditions.attributeContains(findElementByXpath(listOfNecessaryParams[0]),
								listOfNecessaryParams[1], listOfNecessaryParams[2]));
					}
				}
				else
				{
					if (listOfNecessaryParams[2].substring(0, dataRepositoryPrefix.length()) == dataRepositoryPrefix)
					{
						wait.until(
								ExpectedConditions.attributeContains(findElementInRepository(listOfNecessaryParams[0]),
										listOfNecessaryParams[1], data.getDataFromRepository(exec.getScenarioID(),
												listOfNecessaryParams[2].substring(dataRepositoryPrefix.length()))));
					}
					else
					{
						wait.until(
								ExpectedConditions.attributeContains(findElementInRepository(listOfNecessaryParams[0]),
										listOfNecessaryParams[1], listOfNecessaryParams[2]));
					}
				}
				break;
			case "attributeToBe":
				if (listOfNecessaryParams[0].contains("//"))
				{
					if (listOfNecessaryParams[2].substring(0, dataRepositoryPrefix.length()) == dataRepositoryPrefix)
					{
						wait.until(ExpectedConditions.attributeToBe(findElementByXpath(listOfNecessaryParams[0]),
								listOfNecessaryParams[1], data.getDataFromRepository(exec.getScenarioID(),
										listOfNecessaryParams[2].substring(dataRepositoryPrefix.length()))));
					}
					else
					{
						wait.until(ExpectedConditions.attributeToBe(findElementByXpath(listOfNecessaryParams[0]),
								listOfNecessaryParams[1], listOfNecessaryParams[2]));
					}
				}
				else
				{
					if (listOfNecessaryParams[2].substring(0, dataRepositoryPrefix.length()) == dataRepositoryPrefix)
					{
						wait.until(ExpectedConditions.attributeToBe(findElementInRepository(listOfNecessaryParams[0]),
								listOfNecessaryParams[1], data.getDataFromRepository(exec.getScenarioID(),
										listOfNecessaryParams[2].substring(dataRepositoryPrefix.length()))));
					}
					else
					{
						wait.until(ExpectedConditions.attributeToBe(findElementInRepository(listOfNecessaryParams[0]),
								listOfNecessaryParams[1], listOfNecessaryParams[2]));
					}
				}
				break;
			case "attributeToBeNotEmpty":
				if (listOfNecessaryParams[0].contains("//"))
				{
					wait.until(ExpectedConditions.attributeToBeNotEmpty(findElementByXpath(listOfNecessaryParams[0]),
							listOfNecessaryParams[1]));
				}
				else
				{
					wait.until(ExpectedConditions.attributeToBeNotEmpty(
							findElementInRepository(listOfNecessaryParams[0]), listOfNecessaryParams[1]));
				}
				break;
			case "elementSelectionStateToBe":
				if (listOfNecessaryParams[0].contains("//"))
				{
					wait.until(
							ExpectedConditions.elementSelectionStateToBe(findElementByXpath(listOfNecessaryParams[0]),
									Boolean.parseBoolean(listOfNecessaryParams[1])));
				}
				else
				{
					wait.until(ExpectedConditions.elementSelectionStateToBe(
							findElementInRepository(listOfNecessaryParams[0]),
							Boolean.parseBoolean(listOfNecessaryParams[1])));
				}
				break;
			case "elementToBeClickable":
				if (listOfNecessaryParams[0].contains("//"))
				{
					wait.until(ExpectedConditions.elementToBeClickable(findElementByXpath(listOfNecessaryParams[0])));

				}
				else
				{
					wait.until(
							ExpectedConditions.elementToBeClickable(findElementInRepository(listOfNecessaryParams[0])));
				}
				break;
			case "elementToBeSelected":
				if (listOfNecessaryParams[0].contains("//"))
				{
					wait.until(ExpectedConditions.elementToBeSelected(findElementByXpath(listOfNecessaryParams[0])));
				}
				else
				{
					wait.until(
							ExpectedConditions.elementToBeSelected(findElementInRepository(listOfNecessaryParams[0])));
				}
				break;
			case "frameToBeAvailableAndSwitchToIt":
				wait.until(ExpectedConditions.frameToBeAvailableAndSwitchToIt(listOfNecessaryParams[0]));
				break;
			case "invisibilityOf":
				if (listOfNecessaryParams[0].contains("//"))
				{
					wait.until(ExpectedConditions.invisibilityOf(findElementByXpath(listOfNecessaryParams[0])));
				}
				else
				{
					wait.until(ExpectedConditions.invisibilityOf(findElementInRepository(listOfNecessaryParams[0])));
				}
				break;
			case "invisibilityOfAllElements":
				for (String idElement : listOfNecessaryParams)
				{
					if (idElement.contains("//"))
					{
						wait.until(ExpectedConditions.invisibilityOfAllElements(findElementsByXpath(idElement)));
					}
					else
					{
						wait.until(ExpectedConditions.invisibilityOfAllElements(findElementInRepository(idElement)));
					}
				}
				break;
			case "textToBePresentInElement":
				if (listOfNecessaryParams[0].contains("//"))
				{
					wait.until(ExpectedConditions.textToBePresentInElement(findElementByXpath(listOfNecessaryParams[0]),
							listOfNecessaryParams[1]));
				}
				else
				{
					wait.until(ExpectedConditions.textToBePresentInElement(
							findElementInRepository(listOfNecessaryParams[0]), listOfNecessaryParams[1]));
				}
				break;
			case "textToBePresentInElementValue":
				if (listOfNecessaryParams[0].contains("//"))
				{
					wait.until(ExpectedConditions.textToBePresentInElementValue(
							findElementByXpath(listOfNecessaryParams[0]), listOfNecessaryParams[1]));
				}
				else
				{
					wait.until(ExpectedConditions.textToBePresentInElementValue(
							findElementInRepository(listOfNecessaryParams[0]), listOfNecessaryParams[1]));
				}
				break;
			case "urlToBe":
				if (listOfNecessaryParams[0].contains("//"))
				{
					wait.until(ExpectedConditions.urlToBe(listOfNecessaryParams[0]));
				}
				else
				{
					wait.until(ExpectedConditions.urlToBe(exec.getURL(listOfNecessaryParams[0])));
				}
				break;
			case "titleContains":
				if (listOfNecessaryParams[0].substring(0, dataRepositoryPrefix.length()) == dataRepositoryPrefix)
				{
					wait.until(ExpectedConditions.titleContains(data.getDataFromRepository(exec.getScenarioID(),
							listOfNecessaryParams[0].substring(dataRepositoryPrefix.length()))));
				}
				else
				{
					wait.until(ExpectedConditions.titleContains(listOfNecessaryParams[0]));
				}
				break;
			case "titleIs":
				if (listOfNecessaryParams[0].substring(0, dataRepositoryPrefix.length()) == dataRepositoryPrefix)
				{
					wait.until(ExpectedConditions.titleIs(data.getDataFromRepository(exec.getScenarioID(),
							listOfNecessaryParams[0].substring(dataRepositoryPrefix.length()))));
				}
				else
				{
					wait.until(ExpectedConditions.titleIs(listOfNecessaryParams[0]));
				}
				break;
			case "urlContains":
				if (listOfNecessaryParams[0].substring(0, dataRepositoryPrefix.length()) == dataRepositoryPrefix)
				{
					wait.until(ExpectedConditions.urlContains(data.getDataFromRepository(exec.getScenarioID(),
							listOfNecessaryParams[0].substring(dataRepositoryPrefix.length()))));
				}
				else
				{
					wait.until(ExpectedConditions.urlContains(listOfNecessaryParams[0]));
				}
				break;
			case "visibilityOf":
				if (listOfNecessaryParams[0].contains("//"))
				{
					wait.until(ExpectedConditions.visibilityOf(findElementByXpath(listOfNecessaryParams[0])));
				}
				else
				{
					wait.until(ExpectedConditions.visibilityOf(findElementInRepository(listOfNecessaryParams[0])));
				}
				break;
			case "visibilityOfAllElements":
				for (String idElement : listOfNecessaryParams)
				{
					if (idElement.contains("//"))
					{
						wait.until(ExpectedConditions.visibilityOfAllElements(findElementsByXpath(idElement)));
					}
					else
					{
						wait.until(ExpectedConditions.visibilityOfAllElements(findElementInRepository(idElement)));
					}
				}
				break;
		}
	} catch (Exception e) {
		common.quitDriver();
		throw new Exception(exceptionTxt.exceptionText("waitForSeconds"));
	}
}
}

}
