using UnityEngine;

public class SideMovementControls : GameflowBehavior
{
    [SerializeField] private float m_smoothTime = 0.2f;
    [SerializeField] private float m_maxXPosition = 5f;
    [SerializeField] private float m_pixelsPerMeter = 500f;
    
    public Vector3 m_desiredPosition;
    public Vector3 m_startPosition;
    public Vector3 m_startCursorPosition;
    public Vector3 m_currentCursorPosition;
    private Vector3 m_progressionPosition;
    private float m_progression;
    private float m_velocity;
    private bool m_isControlEnabled;

    protected override void OnEnable()
    {
        base.OnEnable();
        Controller.OnTapBegin += OnTapBegin;
        Controller.OnHold += OnHold;
        Controller.OnRelease += OnRelease;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Controller.OnTapBegin -= OnTapBegin;
        Controller.OnHold -= OnHold;
        Controller.OnRelease -= OnRelease;
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        UpdateMovement();
    }

    protected override void OnMainMenu()
    {
        base.OnMainMenu();
        m_isControlEnabled = false;
    }

    protected override void OnGameplay()
    {
        base.OnGameplay();
        m_isControlEnabled = true;
    }

    protected override void OnGameover()
    {
        base.OnGameover();
        m_isControlEnabled = false;
    }

    protected override void OnVictory()
    {
        base.OnVictory();
        m_isControlEnabled = false;
    }

    private void Initialize()
    {
        m_startPosition = transform.position;
        m_desiredPosition = m_startPosition;
        m_progressionPosition = m_startPosition;
    }
    
    private void OnTapBegin(Vector3 cursorPosition)
    {
        m_startPosition = transform.position;
        m_startCursorPosition = cursorPosition;
    }

    private void OnHold(Vector3 cursorPosition)
    {
        m_currentCursorPosition = cursorPosition;

        Vector3 cursorPositionDiff = m_currentCursorPosition - m_startCursorPosition;

        m_desiredPosition = m_startPosition;
        m_desiredPosition.x = m_startPosition.x + cursorPositionDiff.x * (1f/m_pixelsPerMeter);
        
        m_desiredPosition.x = Mathf.Clamp(m_desiredPosition.x, -m_maxXPosition, m_maxXPosition);
    }

    private void OnRelease(Vector3 cursorPosition)
    {
        m_startPosition = transform.position;
        m_startCursorPosition = cursorPosition;
        m_currentCursorPosition = cursorPosition;
        m_desiredPosition = transform.position;
    }


    private void UpdateMovement()
    {
        if(m_isControlEnabled == false)
            return;
        
        m_progressionPosition.x = Mathf.SmoothDamp(m_progressionPosition.x, m_desiredPosition.x, ref m_velocity,
            m_smoothTime);
        transform.position = m_progressionPosition;
    }
}