﻿// Copyright (c) 2016-2017 Alexander Ong
// See LICENSE in project root for license information.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note2D3DSelector : MonoBehaviour {
    public NoteController nCon;
    public NoteVisuals2DManager note2D;
    public NoteVisuals3DManager note3D;
    public Skin customSkin;

    public NoteVisualsManager currentVisualsManager
    {
        get
        {
            return note2D.gameObject.activeSelf ? note2D : (NoteVisualsManager)note3D;
        }
    }
    
    void Start()
    {
        /*
        if (AssignCustomResources.noteSpritesAvaliable != null)
        {
            switch (AssignCustomResources.noteSpritesAvaliable)
            {
                //case (Skin.AssestsAvaliable.All):
                    //Set2D();
                    //enabled = false;
                    //break;
                case (Skin.AssestsAvaliable.None):
                    Set3D();
                    //enabled = false;
                    break;
                default:
                    break;
            }
        }*/

        UpdateSelectedGameObject();
    }

    public void UpdateSelectedGameObject()
    {
        if (Globals.viewMode == Globals.ViewMode.Chart)
        {
            if (CheckTextureInSkin())
                Set2D();
            else
                Set3D();
            /*
            if (Globals.ghLiveMode)
                Set2D();
            else
            {
                switch (AssignCustomResources.noteSpritesAvaliable)
                {
                    case (Skin.AssestsAvaliable.All):
                        Set2D();
                        break;
                    case (Skin.AssestsAvaliable.None):
                        Set3D();
                        break;
                    default:
                        if (CheckTextureInSkin())
                            Set2D();
                        else
                            Set3D();
                        break;
                }
            }*/
        }
    }

    void Set2D()
    {
        note2D.gameObject.SetActive(true);
        note3D.gameObject.SetActive(false);
    }

    void Set3D()
    {
        note2D.gameObject.SetActive(false);
        note3D.gameObject.SetActive(true);
    }

    bool CheckTextureInSkin()
    {
        Texture2D textureInSkin = null;

        Note note = nCon.note;
        Note.Note_Type noteType = note.type;
        Note.Special_Type specialType = NoteVisualsManager.IsStarpower(note);

        int arrayPos = GetSpriteArrayPos(note);
        if (Globals.ghLiveMode)
        {
            if (noteType == Note.Note_Type.Strum)
            {
                if (specialType == Note.Special_Type.STAR_POW)
                    textureInSkin = customSkin.sp_strum_ghl[arrayPos];
                else
                    textureInSkin = customSkin.reg_strum_ghl[arrayPos];
            }
            else if (noteType == Note.Note_Type.Hopo)
            {
                if (specialType == Note.Special_Type.STAR_POW)
                    textureInSkin = customSkin.sp_hopo_ghl[arrayPos];
                else
                    textureInSkin = customSkin.reg_hopo_ghl[arrayPos];
            }
            // Tap notes
            else
            {
                if (!note.IsOpenNote())
                {
                    if (specialType == Note.Special_Type.STAR_POW)
                        textureInSkin = customSkin.sp_tap_ghl[arrayPos];
                    else
                        textureInSkin = customSkin.reg_tap_ghl[arrayPos];
                }
            }   
        }
        else
        {
            if (noteType == Note.Note_Type.Strum)
            {
                if (specialType == Note.Special_Type.STAR_POW)
                    textureInSkin = customSkin.sp_strum[arrayPos];
                else
                    textureInSkin = customSkin.reg_strum[arrayPos];
            }
            else if (noteType == Note.Note_Type.Hopo)
            {
                if (specialType == Note.Special_Type.STAR_POW)
                    textureInSkin = customSkin.sp_hopo[arrayPos];
                else
                    textureInSkin = customSkin.reg_hopo[arrayPos];
            }
            // Tap notes
            else
            {
                if (note.fret_type != Note.Fret_Type.OPEN)
                {
                    if (specialType == Note.Special_Type.STAR_POW)
                        textureInSkin = customSkin.sp_tap[arrayPos];
                    else
                        textureInSkin = customSkin.reg_tap[arrayPos];
                }
            }
            
        }

        return textureInSkin;
    }

    protected int GetSpriteArrayPos(Note note)
    {
        int arrayPos = note.rawNote;

        if (Globals.ghLiveMode)
        {
            arrayPos = 0;

            if (note.ghlive_fret_type >= Note.GHLive_Fret_Type.WHITE_1 && note.ghlive_fret_type <= Note.GHLive_Fret_Type.WHITE_3)
                arrayPos = 1;
            else if (note.IsOpenNote())
                arrayPos = 2;
        }

        return arrayPos;
    }
}
