using System;
using DG.Tweening;
using Fsi.Gameplay.Visuals;
using UnityEngine;

namespace Fsi.NodeMap.Character
{
    public class Character : MonoBehaviour
    {
        [SerializeField]
        private CharacterVisuals visuals;
        
        public CharacterVisuals Visuals => visuals;

        [Header("Ease")]
        
        [SerializeField]
        private Ease moveEase;

        [SerializeField]
        private float moveTime = 1;

        [SerializeField]
        private AnimationCurve speedCurve;

        public void Initialize(CharacterVisuals characterVisuals)
        {
            if (visuals)
            {
                Destroy(visuals.gameObject);
            }

            visuals = Instantiate(visuals, transform);
        }

        public void MoveTo(Vector3 position, Action callback)
        {
            float timer = 0;
            transform.LookAt(position);
            transform.DOMove(position, moveTime)
                     .SetEase(moveEase)
                     .OnUpdate(() =>
                               {
                                   timer += Time.deltaTime;
                                   float t = timer / moveTime;
                                   visuals.SetMovement(speedCurve.Evaluate(t) * transform.forward, false);
                               })
                     .OnComplete(() =>
                                 {
                                     visuals.SetMovement(Vector3.zero, false);
                                     transform.forward = Vector3.forward;
                                     callback?.Invoke();
                                 });
        }
    }
}