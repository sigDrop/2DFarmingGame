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

    private Vector2Int _gridPosition;

    public CropController.CropType cropType;
    public float growFailChance;

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

        UpdateGridInfo();

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

    public void PlantCrop(CropController.CropType cropToPlant)
    {
        if (currentStage == GrowthStage.ploughed && isWatered && !preventUse)
        {
            currentStage = GrowthStage.planted;

            cropType = cropToPlant;

            growFailChance = CropController.instance.GetCropInfo(cropType).growthFailChance;

            CropController.instance.UseSeed(cropToPlant);

            UpdateCropSprite();
        }
    }

    public void UpdateCropSprite()
    {
        CropInfo activeCrop = CropController.instance.GetCropInfo(cropType);


        switch (currentStage)
        {
            case GrowthStage.planted:
                cropSpriteRenderer.sprite = activeCrop.planted;
                break;

            case GrowthStage.growing1:
                cropSpriteRenderer.sprite = activeCrop.growStage1;
                break;

            case GrowthStage.growing2:
                cropSpriteRenderer.sprite = activeCrop.growStage2;
                break;

            case GrowthStage.ripe:
                cropSpriteRenderer.sprite = activeCrop.ripe;
                break;
        }

        UpdateGridInfo();
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

            CropController.instance.AddCrop(cropType);
        }
    }

    public void SetGridPosition(int x, int y)
    {
        _gridPosition = new Vector2Int(x, y);
    }

    void UpdateGridInfo()
    {
        GridInfo.instance.UpdateInfo(this, _gridPosition.x, _gridPosition.y);
    }
        
}
