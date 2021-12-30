using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VoidPixel
{
    public class Fill : MonoBehaviour
    {        
        public int Value;
        [SerializeField] TMP_Text valueTxt;
        [SerializeField] float speed = 1000;
        [SerializeField] ParticleSystem combineParticles;
        Image image;
        bool hasCombined;

        private void Update()
        {
            if (transform.localPosition != Vector3.zero)
            {
                hasCombined = false;
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, speed * Time.deltaTime);
            }
            else if (!hasCombined)
            {
                if (transform.parent.GetChild(0) != this.transform) { Destroy(transform.parent.GetChild(0).gameObject); }
                hasCombined = true;
            }
        }

        public void UpdateFillValue(int valueIn)
        {
            Value = valueIn;
            valueTxt.text = valueIn.ToString();

            int colorIndex = GetColorIndex(Value);
            image = GetComponent<Image>();
            image.color = GameController.instance.fillColors[colorIndex];
        }

        public void Double()
        {
            Value *= 2;
            GameController.instance.UpdateScore(Value);
            valueTxt.text = Value.ToString();
            combineParticles.Play();

            int colorIndex = GetColorIndex(Value);
            image.color = GameController.instance.fillColors[colorIndex];

            GameController.instance.WinCheck(Value);
            
        }

        int GetColorIndex(int valueIn)
        {
            int index = 0;
            while (valueIn != 1)
            {
                index++;
                valueIn /= 2;
            }

            index--;
            return index;
        }
    }
}

