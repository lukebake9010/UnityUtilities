using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Utilities.StepGame.Character
{
    public abstract class CharacterAction
    {
        public bool IsComplete { get; protected set; } = false;

        public abstract Task Run(StepGamePlayerController character);
    }
}
