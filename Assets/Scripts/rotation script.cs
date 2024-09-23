using UnityEngine;

public class PlanetarySimulation : MonoBehaviour
{
    public Transform sun;
    public Transform earth;
    public Transform moon;

    [Header("Rotation Speeds")]
    public float sunRotationSpeed = 10f;
    public float earthRotationSpeed = 30f;
    public float moonRotationSpeed = 50f;

    [Header("Orbit Speeds")]
    public float earthOrbitSpeed = 5f;
    public float moonOrbitSpeed = 10f;

    [Header("Orbit Radii")]
    public float earthOrbitRadius = 5f;
    public float moonOrbitRadius = 2f;

    [Header("Planet Scales")]
    public float sunScale = 2f;
    public float earthScale = 1f;
    public float moonScale = 0.5f;

    private float earthAngle = 0f;
    private float moonAngle = 0f;

    void Start()
    {
        InitializePlanets();
    }

    void InitializePlanets()
    {
        // Set scales
        sun.localScale = Vector3.one * sunScale;
        earth.localScale = Vector3.one * earthScale;
        moon.localScale = Vector3.one * moonScale;

        // Set initial positions
        sun.position = Vector3.zero;
        earth.position = sun.position + new Vector3(earthOrbitRadius, 0, 0);
        moon.position = earth.position + new Vector3(moonOrbitRadius, 0, 0);

        // Set initial rotation (optional)
        sun.rotation = Quaternion.identity;
        earth.rotation = Quaternion.identity;
        moon.rotation = Quaternion.identity;
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;

        // Rotate objects on their own axis
        sun.rotation *= Quaternion.Euler(0, sunRotationSpeed * deltaTime, 0);
        earth.rotation *= Quaternion.Euler(0, earthRotationSpeed * deltaTime, 0);
        moon.rotation *= Quaternion.Euler(0, moonRotationSpeed * deltaTime, 0);

        // Orbit Earth around Sun
        earthAngle += earthOrbitSpeed * deltaTime;
        float earthX = Mathf.Cos(earthAngle) * earthOrbitRadius;
        float earthZ = Mathf.Sin(earthAngle) * earthOrbitRadius;
        earth.position = sun.position + new Vector3(earthX, 0, earthZ);

        // Orbit Moon around Earth
        moonAngle += moonOrbitSpeed * deltaTime;
        float moonX = Mathf.Cos(moonAngle) * moonOrbitRadius;
        float moonZ = Mathf.Sin(moonAngle) * moonOrbitRadius;
        moon.position = earth.position + new Vector3(moonX, 0, moonZ);
    }
}