using UnityEngine;


namespace PotatoMode.Utilities.Constants
{
    public static class Animation
    {
        public static readonly int IS_RUNNING = Animator.StringToHash("IsRunning");
        public static readonly int IS_DASHING = Animator.StringToHash("IsDashing");
        public static readonly int GAME_END = Animator.StringToHash("GameEnd");
        public static readonly int IN_MOVE = Animator.StringToHash("InMove");
        public static readonly int IN_AIR = Animator.StringToHash("InAir");
        public static readonly int JUMP = Animator.StringToHash("Jump");
    }
}