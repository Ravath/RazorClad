using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using System.IO;

[XmlRoot("DialogCollection")]
public class DialogCollection {

	[XmlArray("Dialogs")]
	[XmlArrayItem("Dialog")]
	public List<Dialog> Dialogs = new List<Dialog>();

	public static DialogCollection Load( string xmlPath ) {
		TextAsset _xml = Resources.Load<TextAsset>(xmlPath);
		XmlSerializer serialiser = new XmlSerializer(typeof(DialogCollection));
		StringReader sr = new StringReader(_xml.text);
		DialogCollection dialogs = serialiser.Deserialize(sr) as DialogCollection;
		sr.Close();
		return dialogs;
	}
}

public class Dialog {
	[XmlAttribute("Code")]
	public string code;

	[XmlArray("Repartees")]
	[XmlArrayItem("Repartee")]
	public List<Repartee> Reparties = new List<Repartee>();
}

public class Repartee {
	[XmlAttribute("Locuteur")]
	public PersonnagesEnum Locuteur { get; set; }
	[XmlAttribute("Left")]
	public bool LeftSide { get; set; }
	[XmlElement("Message")]
	public string Message { get; set; }
}
