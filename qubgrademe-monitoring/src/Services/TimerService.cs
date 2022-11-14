using System.Timers;

namespace src.Services;
using Timer = System.Timers.Timer;

public class TimerService
{
    private const int TIME = 300;
    private int _counter;
    private readonly Timer _timer;
    private Action TimerDone;

    public TimerService()
    {
        _counter = TIME;
        _timer = new Timer();
        _timer.Interval = 1000;
        _timer.Elapsed += TimerOnElapsed;
        _timer.Start();
    }
    
    private void TimerOnElapsed(object? sender, ElapsedEventArgs e)
    {
        // Subtract 1 from the counter
        _counter--;
        // If the counter is 0
        if (_counter != 0) return;
        // Reset the counter
        TimerDone();
        _counter = TIME;
    }
    
    public void AddTimerDone(Action timerDone)
    {
        TimerDone = timerDone;
    }
    
    public void SetIntervalMethod(Action<int> method)
    {
        _timer.Elapsed += (sender, args) =>
        {
            method(_counter);
        };
    }
}