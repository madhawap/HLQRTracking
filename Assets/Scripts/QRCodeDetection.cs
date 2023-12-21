using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Microsoft.MixedReality.OpenXR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using Microsoft.MixedReality.OpenXR;
using TMPro;

public class QRCodeDetection : MonoBehaviour
{
    public GameObject mainText;
    
    [SerializeField]
    private ARMarkerManager markerManager;

    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        // Subscribe to the markersChanged event
        markerManager.markersChanged += OnMarkersChanged;
        
        textMeshPro = mainText.GetComponent<TextMeshProUGUI>();
    }

    private void OnMarkersChanged(ARMarkersChangedEventArgs args)
    {
        foreach (var addedMarker in args.added)
        {
            // Handle newly added markers
            Debug.Log($"QR Code Detected! Marker ID: {addedMarker.trackableId}");
            // Debug.Log($"I found {addedMarker.GetDecodedString()}");
            // You can access more information about the marker using addedMarker properties
            // For example, addedMarker.GetDecodedString() or addedMarker.GetQRCodeProperties()
        }

        foreach (var updatedMarker in args.updated)
        {
            // Handle updated markers
            // You can access information about the marker using updatedMarker properties
            // Debug.Log($"QR Code updated! Marker ID: {updatedMarker}");
            // Debug.Log("Yay! Found it.");
            
            // Get the decoded string from the added marker
            string qrCodeString = updatedMarker.GetDecodedString();
            
            // Set the QR code string to the TextMeshPro component
            if (textMeshPro != null)
            {
                textMeshPro.text = qrCodeString;
            }
        }

        foreach (var removedMarkerId in args.removed)
        {
            // Handle removed markers
            Debug.Log($"QR Code Removed! Marker ID: {removedMarkerId}");
            
            // Clear the TextMeshPro text when a marker is removed
            if (textMeshPro != null)
            {
                // textMeshPro.text = string.Empty;
                textMeshPro.text = " ";
            }
        }
    }
}