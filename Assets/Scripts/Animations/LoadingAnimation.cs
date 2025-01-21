using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Animations
{
    public class LoadingAnimation : MonoBehaviour
    {
        [field: SerializeField] private float _boundaryPosValue;
        [field: SerializeField] private RectTransform _topObject;
        [field: SerializeField] private RectTransform _botObject;
        
        private CancellationTokenSource _cts;
        private Sequence _sequence;

        public async UniTask StartAnimationAsync(float duration)
        {
            SetDefault();
            try
            {
                _cts = new CancellationTokenSource();
                _sequence = DOTween.Sequence();
                _sequence
                    .Append(_topObject.DOLocalMoveX(_boundaryPosValue, duration))
                    .Join(_botObject.DOLocalMoveX(-_boundaryPosValue, duration))
                    .Append(_topObject.DOLocalMoveX(-_boundaryPosValue, duration))
                    .Join(_botObject.DOLocalMoveX(+_boundaryPosValue, duration))
                    .SetAutoKill(false);

                await _sequence
                    .SetEase(Ease.Linear)
                    .SetLoops(-1, LoopType.Restart)
                    .WithCancellation(_cts.Token);
            }
            catch (OperationCanceledException exception)
            {
                _sequence?.Kill();
                SetDefault();
            }
        }

        public void FinishAnimation()
        {
            _sequence?.Kill();
            SetDefault();
            
            if (_cts != null)
            {
                _cts.Dispose();
                _cts = null;
            }
        }

        private void SetDefault()
        {
            _topObject.localPosition = new Vector3(-_boundaryPosValue, 0,0);
            _botObject.localPosition = new Vector3(_boundaryPosValue, 0,0);
        }
        
        private void OnDisable()
        {
            if (_cts != null)
            {
                _cts.Dispose();
                _cts = null;
            }
        }
    }
}