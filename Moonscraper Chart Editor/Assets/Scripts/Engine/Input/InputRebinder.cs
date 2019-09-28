﻿using System.Collections.Generic;

namespace MSE
{
    namespace Input
    {
        public class InputRebinder
        {
            IEnumerable<InputAction> allActions;
            IInputMap mapCopy;
            IInputMap mapToRebind;
            InputAction actionToRebind;
            IInputDevice device;

            public InputRebinder(InputAction actionToRebind, IInputMap mapToRebind, IEnumerable<InputAction> allActions, IInputDevice device)
            {
                mapCopy = mapToRebind.Clone();
                mapToRebind.SetEmpty();

                this.actionToRebind = actionToRebind;
                this.mapToRebind = mapToRebind;
                this.allActions = allActions;
                this.device = device;            
            }

            public bool TryMap(out InputAction conflict, out IInputMap attemptedInput)
            {
                conflict = null;

                IInputMap currentInput = device.GetCurrentInput();
                attemptedInput = currentInput;

                if (currentInput != null)
                {
                    foreach (InputAction inputAction in allActions)
                    {
                        if (MSChartEditorInput.Category.interactionMatrix.TestInteractable(inputAction.properties.category, actionToRebind.properties.category) && inputAction.HasConflict(currentInput))
                        {
                            conflict = inputAction;
                            return false;
                        }
                    }

                    // Do rebind and exit
                    mapToRebind.SetFrom(currentInput);
                    return true;
                }

                return false;
            }

            public void RevertMapBeingRebound()
            {
                mapToRebind.SetFrom(mapCopy);
            }
        }
    }
}
