using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine.UI;

public class Section{

	private List<XmlNode> _nodes = new List<XmlNode>();

	private GameObject _indexObject;
	private Image _indexImage;

	public Section(GameObject indexObject){
		_indexObject = indexObject;
		_indexImage = _indexObject.GetComponent<Image>();
	}

	public void AddNode(XmlNode node){
		_nodes.Add(node);
	}

	public void SetActive(){
		_indexImage.color = MainController.IndexActiveColor;
	}

	public void SetInactive(){
		_indexImage.color = MainController.IndexInactiveColor;
	}

}
