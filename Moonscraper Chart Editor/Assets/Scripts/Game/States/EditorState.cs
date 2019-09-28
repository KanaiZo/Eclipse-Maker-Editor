﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EditorState : SystemManagerState
{
    public override void Update()
    {
        base.Update();

        ChartEditor editor = ChartEditor.Instance;
        Services services = editor.services;
        Globals.ViewMode viewMode = Globals.viewMode;

        if (MSChartEditorInput.GetInputDown(MSChartEditorInputActions.StepIncrease))
            GameSettings.snappingStep.Increment();

        else if (MSChartEditorInput.GetInputDown(MSChartEditorInputActions.StepDecrease))
            GameSettings.snappingStep.Decrement();

        if (editor.groupMove.movementInProgress)
            return;

        if (services.CanPlay())
        {
            var gamepad = InputManager.Instance.mainGamepad;

            if (MSChartEditorInput.GetInputDown(MSChartEditorInputActions.PlayPause))
            {
                editor.Play();
                return;
            }
            else if (gamepad != null && gamepad.GetButtonPressed(MSE.Input.GamepadDevice.Button.Start))
            {
                editor.StartGameplay();
                return;
            }
        }

        if (MSChartEditorInput.GetInputDown(MSChartEditorInputActions.Delete) && editor.selectedObjectsManager.currentSelectedObjects.Count > 0)
            editor.Delete();

        else if (MSChartEditorInput.GetInputDown(MSChartEditorInputActions.SelectAll))
        {
            editor.toolManager.ChangeTool(EditorObjectToolManager.ToolID.Cursor);
            editor.selectedObjectsManager.SelectAllInView(viewMode);
        }
        else if (MSChartEditorInput.GetInputDown(MSChartEditorInputActions.SelectAllSection))
        {
            editor.toolManager.ChangeTool(EditorObjectToolManager.ToolID.Cursor);
            editor.selectedObjectsManager.HighlightCurrentSection(viewMode);
        }

        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            bool success = false;

            if (MSChartEditorInput.GetInputDown(MSChartEditorInputActions.ActionHistoryUndo))
            {
                if (!editor.commandStack.isAtStart && editor.services.CanUndo())
                {
                    editor.UndoWrapper();
                    success = true;
                }
            }
            else if (MSChartEditorInput.GetInputDown(MSChartEditorInputActions.ActionHistoryRedo))
            {
                if (!editor.commandStack.isAtEnd && editor.services.CanRedo())
                {
                    editor.RedoWrapper();
                    success = true;
                }
            }

            if (success)
            {
                EventSystem.current.SetSelectedGameObject(null);
                editor.groupSelect.reset();
                TimelineHandler.Repaint();
            }
        }

        if (editor.selectedObjectsManager.currentSelectedObjects.Count > 0)
        {
            if (MSChartEditorInput.GetInputDown(MSChartEditorInputActions.ClipboardCut))
                editor.Cut();
            else if (MSChartEditorInput.GetInputDown(MSChartEditorInputActions.ClipboardCopy))
                editor.Copy();
        }
    }
}
