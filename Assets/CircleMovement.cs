using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public Transform centerPoint; // The center point of the circle
    public Transform crowFrontal; // The center point of the circle
    public float radius = 5f; // The radius of the circle
    public float speed = 2f; // The speed of movement around the circle
    public float amplitude = 1f; // The amplitude of the zigzag motion
    public float amplitudeX = 1f; // The amplitude of the zigzag motion in the X direction
    public float amplitudeZ = 1f; // The amplitude of the zigzag motion in the Z direction
    private float angle; // The current angle around the circle
    public float snakeFrequency = 1f; // The frequency of the snake motion
    public float coilFrequency = 2f; // The frequency of the coil motion
    public float coilDepth = 0.5f; // The depth of the coil motion
    private Vector3 prevPosition; // The position of the object in the previous frame

    [Header("DEVIATION")]
    [SerializeField] private float _deviationAmount = 50;
    [SerializeField] private float _deviationSpeed = 2;
    private Vector3 _standardPrediction, _deviatedPrediction;

    private void Start()
    {
        prevPosition = transform.position;

    }
    void Update()
    {

        CircleAround();
        
    }

    private void CircleAround()
    {
        // Calculate the new position on the circle based on the current angle
        float x = centerPoint.position.x + Mathf.Cos(angle) * radius; 
        float z = centerPoint.position.z + Mathf.Sin(angle) * radius;
        // AddDeviation(Time.deltaTime);



        // Move the object to the new position
        Vector3 newPosition = new Vector3(x, transform.position.y, z) ;
        transform.position = newPosition;
        Vector3 direction = newPosition - prevPosition;
        AddDeviation(angle);
        if (direction != Vector3.zero)
        {
            // Rotate the object to face in the direction it is moving
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }

        // Set the previous position to the current position
        prevPosition = transform.position;
        // Increment the angle based on the speed and delta time
        angle += speed * Time.deltaTime;

        // Make sure the angle stays within the range of 0 to 2*pi
        if (angle > Mathf.PI * 2f)
        {
            angle -= Mathf.PI * 2f;
        }
    }

    private void AddDeviation(float leadTimePercentage)
    {
        var deviationX = Mathf.Sin(Time.time * _deviationSpeed) *_deviationAmount;
        var deviationZ = Mathf.Cos(Time.time * _deviationSpeed) * _deviationAmount;
       var actualPos= transform.TransformPoint(new Vector3(deviationX, 0, 5));
       // crowFrontal.localPosition = new Vector3(deviationX, 0, crowFrontal.localPosition.z);
       // _deviatedPrediction = crowFrontal.position;
        _deviatedPrediction = actualPos;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(centerPoint.transform.position, radius);
        Gizmos.DrawLine(transform.position, _deviatedPrediction);
    }
}
