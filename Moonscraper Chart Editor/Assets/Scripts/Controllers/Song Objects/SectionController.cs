﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class SectionController : SongObjectController
{
    public SectionGuiController sectionGui;
    Section section;
    Text sectionText;

    void Awake()
    {
        sectionText = GetComponent<Text>();
    }

    public override void UpdateSongObject()
    {
        transform.position = new Vector3(5, section.song.ChartPositionToWorldYPosition(section.position), 0);

        sectionText.text = section.title;
    }

    public override void Delete()
    {
        Destroy(sectionGui);

        section.song.Remove(section);
  
        Destroy(gameObject);
    }

    public void Init(MovementController movement, Section _section, TimelineHandler _timelineHandler, GameObject bpmGuiParent)
    {
        Init(movement);
        section = _section;
        sectionGui.Init(_section, _timelineHandler, movement, bpmGuiParent);
    }
}
