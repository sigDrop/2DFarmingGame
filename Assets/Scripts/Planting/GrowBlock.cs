using UnityEngine;
using UnityEngine.InputSystem;

public class GrowBlock : MonoBehaviour
{
    public enum GrowthStage
    {
        barren,
        ploughed,
        planted,
        growing1,
        growing2,
        ripe
    }

    public GrowthStage currentStage;

    public SpriteRenderer spriteRenderer;
    public Sprite soilTilled, soilWatered;

    public SpriteRenderer cropSpriteRenderer;
    public Sprite cropPlanted, cropGrowing1, cropGrowing2, cropRipe;

    public bool isWatered = false;

    public bool preventUse;

    void Update()
    {
        /*if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            AdvanceStage();

            SetSoilSprite();
        }*/

#if UNITY_EDITOR
        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            AdvanceCrop();
        }
#endif
    }


    public void AdvanceStage()
    {
        currentStage++;

        if ((int) currentStage >=6)
        {
            currentStage = GrowthStage.barren;
        }
    }

    public void SetSoilSprite()
    {
        if(currentStage == GrowthStage.barren)
        {
            spriteRenderer.sprite = null;
        }
        else
        {
            if(isWatered)
            {
                spriteRenderer.sprite = soilWatered;
            }
            else
            {
                spriteRenderer.sprite = soilTilled;
            }
        }
    }

    public void PloughSoil()
    {
        if (currentStage == GrowthStage.barren && !preventUse)
        {
            currentStage = GrowthStage.ploughed;

            SetSoilSprite();
        }
    }

    public void WaterSoil()
    {
        if (!preventUse)
        {
            isWatered = true;

            SetSoilSprite();
        }
    }

    public void PlantCrop()
    {
        if (currentStage == GrowthStage.ploughed && isWatered && !preventUse)
        {
            currentStage = GrowthStage.planted;

            UpdateCropSprite();
        }
    }

    void UpdateCropSprite()
    {
        switch (currentStage)
        {
            case GrowthStage.planted:
                cropSpriteRenderer.sprite = cropPlanted;
                break;

            case GrowthStage.growing1:
                cropSpriteRenderer.sprite = cropGrowing1;
                break;

            case GrowthStage.growing2:
                cropSpriteRenderer.sprite = cropGrowing2;
                break;

            case GrowthStage.ripe:
                cropSpriteRenderer.sprite = cropRipe;
                break;
        }
    }

    public void AdvanceCrop()
    {
        if (isWatered && !preventUse)
        {
            if (currentStage == GrowthStage.planted || currentStage == GrowthStage.growing1 || currentStage == GrowthStage.growing2)
            {
                currentStage++;
                
                isWatered = false;

                SetSoilSprite();
                UpdateCropSprite();
            }
        }
    }

    public void HarvestCrop()
    {
        if (currentStage == GrowthStage.ripe && !preventUse)
        {
            currentStage = GrowthStage.ploughed;

            SetSoilSprite();

            cropSpriteRenderer.sprite = null;
        }
    }
        
}
