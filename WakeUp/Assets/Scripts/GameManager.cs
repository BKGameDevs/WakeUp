using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PlayerController Player;

    public Camera mainCam;
	public Camera secondCam;
	public bool onMainCam;
	public string camName;

    internal bool KeyObtained { get; private set;}

    // Start is called before the first frame update

    public GameEvent OnLevelStart;
    void Start()
    {
        onMainCam = true;
		camName = mainCam.name;

        OnLevelStart?.Raise();
        SectionManager.PlayerSpawned += SectionManager_PlayerSpawned;
    }

    private void SectionManager_PlayerSpawned(object sender, System.EventArgs e)
    {
        KeyObtained = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetKeyObtained()
    {
        KeyObtained = true;
    }

    public void ToggleCamera(){
		if (onMainCam) {
			mainCam.enabled = false;
			secondCam.enabled = true;
			onMainCam = !onMainCam;
		}
		else if (!onMainCam) {
			mainCam.enabled = true;
			secondCam.enabled = false;
			onMainCam = !onMainCam;
		}
	}

    public void ToggleCameraWithCrossfade(){
        
            CrossFadeController.Instance.RunCrossFade(() => {
                    GameManager.Instance.ToggleCamera();
                    // this.StartTimedAction(null,
                    // () => {
                    //     CrossFadeController.Instance.RunCrossFade(() => {
                    //         GameManager.Instance.ToggleCamera();
                    //     });
                    // }, 3f);
            });

    }
}
