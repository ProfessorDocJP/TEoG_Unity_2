﻿public class WillSlider : BasicSlider
{
    private void changeWill()
    {
        slider.value = basicChar.WP.Slider();
        if (TextMesh != null)
        {
            TextMesh.text = basicChar.WP.Status();
        }
    }

    private void OnDisable()
    {
        Health.updateSlider -= changeWill;
    }

    private void OnEnable()
    {
        Health.updateSlider += changeWill;
        basicChar.WP.manualSliderUpdate();
    }
}