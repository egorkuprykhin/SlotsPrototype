using FSM;
using SceneData;
using UnityEngine;
using VContainer;
using View;

namespace States
{
    public class MenuState : IState
    {
        [Inject] private readonly StateMachine _stateMachine;
        [Inject] private readonly GameSceneData _sceneData;

        [Inject] private readonly PolicyScreenView _policyScreenView;
        [Inject] private readonly MenuView _menuView;


        public void Enter()
        {
            _sceneData.MenuCamera.gameObject.SetActive(true);
            _sceneData.BackSoundAudioSource.Play();
            _menuView.Show();
            
            SubscribeMenuButtons();
            SubscribePolicyButton();
        }

        public void Exit()
        {
            _menuView.Hide();
            _sceneData.BackSoundAudioSource.Stop();
            _sceneData.MenuCamera.gameObject.SetActive(false);
        }

        
        public void StartGame() => 
            PlayGameButtonPressed();

        public void ShowPolicy() => 
            ShowPolicyButtonPressed();

        private void PlayGameButtonPressed()
        {
            _stateMachine.Enter<GameplayState>();
        }

        private void SettingsButtonPressed()
        {
        }

        private void ShowPolicyButtonPressed()
        {
            _policyScreenView.Show();
        }

        private void ExitButtonPressed()
        {
            Application.Quit();
        }

        private void PolicyBackToMenu()
        {
            _policyScreenView.Hide();
        }

        private void SubscribeMenuButtons()
        {
            _menuView.PlayButton.onClick.RemoveAllListeners();
            _menuView.PlayButton.onClick.AddListener(PlayGameButtonPressed);
            
            _menuView.SettingsButton.onClick.RemoveAllListeners();
            _menuView.SettingsButton.onClick.AddListener(SettingsButtonPressed);
            
            _menuView.ShowPolicyButton.onClick.RemoveAllListeners();
            _menuView.ShowPolicyButton.onClick.AddListener(ShowPolicyButtonPressed);
            
            _menuView.ExitButton.onClick.RemoveAllListeners();
            _menuView.ExitButton.onClick.AddListener(ExitButtonPressed);
        }

        private void SubscribePolicyButton()
        {
            _policyScreenView.BackToMenuButton.onClick.RemoveAllListeners();
            _policyScreenView.BackToMenuButton.onClick.AddListener(PolicyBackToMenu);
        }
    }
}