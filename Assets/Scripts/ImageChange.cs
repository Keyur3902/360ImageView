
// using UnityEngine;

// public class ImageChange : MonoBehaviour
// {
//     public Texture[] images; // An array to hold your images.
//     private int currentImageIndex = 0;

//     void Start()
//     {
//         // Initialize the plane's texture with the first image.
//         GetComponent<Renderer>().material.mainTexture = images[0];
//     }

//     void Update()
//     {
//         // Check for touch input.
//         if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
//         {
//             // Change the texture to the next image.
//             currentImageIndex = (currentImageIndex + 1) % images.Length;
//             GetComponent<Renderer>().material.mainTexture = images[currentImageIndex];
//         }
//     }
// }


using UnityEngine;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;

public class ImageChange : MonoBehaviour
{
    public Texture[] images; // An array to hold your images.
    private int currentImageIndex = 0;
    private float initialRotation;
    private float accumulatedRotation = 0.0f;
    // public TextMeshPro angleText;
    // public TextMeshPro imageText;
    private float rotationThreshold = 0.05f;
    

    void Start()
    {
        print("Hello World");
        // Initialize the plane's texture with the first image.
        GetComponent<Renderer>().material.mainTexture = images[0];

        // Check if gyroscope is available on the device.
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true; // Enable the gyroscope.
            initialRotation = Input.gyro.attitude.eulerAngles.x;
        }
        else
        {
            Debug.LogError("Gyroscope not supported on this device.");
        }
    }


    // void Update()
    // {
    //     // Check for gyroscope input.
    //     if (SystemInfo.supportsGyroscope)
    //     {
    //         // Get the current rotation.
    //         Quaternion rotation = Input.gyro.attitude;

    //         // Convert the rotation to Euler angles.
    //         Vector3 angles = rotation.eulerAngles;

    //         float normalizedRotation = angles.x % 360;

    //         // Calculate the rotation difference from the initial rotation.
    //         float rotationDifference = normalizedRotation - accumulatedRotation;

    //         int rotationSign = Mathf.RoundToInt(Mathf.Sign(rotationDifference));
    //         angleText.text = rotationSign.ToString();

    //         if (Mathf.Abs(rotationDifference) >= 2.0f)
    //         {
    //             // Update the accumulated rotation.
    //             accumulatedRotation = normalizedRotation;

    //             // Increment the image index.
    //             currentImageIndex = (currentImageIndex + 1) % images.Length;

    //             // Update the displayed image.
    //             GetComponent<Renderer>().material.mainTexture = images[currentImageIndex];

    //             // Update the UI text.
    //             imageText.text = currentImageIndex.ToString();
    //         }
    //     }
    // }

    void Update()
    {
        // Check for gyroscope input.
        if (SystemInfo.supportsGyroscope)
        {
            float rotationRateY = Input.gyro.rotationRateUnbiased.y;

            accumulatedRotation += rotationRateY * Time.deltaTime;

            if (Mathf.Abs(accumulatedRotation) >= rotationThreshold)
            {
                int rotationDirection = Mathf.RoundToInt(Mathf.Sign(accumulatedRotation));

                currentImageIndex = (currentImageIndex + rotationDirection + images.Length) % images.Length;

                GetComponent<Renderer>().material.mainTexture = images[currentImageIndex];

                accumulatedRotation = 0.0f;
            }
        }
    }

}