using System;
using UnityEngine;
using UnityEngine.UI;

public class CalendarManager : MonoBehaviour
{
    public Text monthYearText;
    public GameObject dayButtonPrefab;
    public Transform calendarGrid;

    private DateTime currentDate;

    void Start()
    {
        currentDate = DateTime.Today;
        GenerateCalendar(currentDate);
    }

    public void PreviousMonth()
    {
        currentDate = currentDate.AddMonths(-1);
        GenerateCalendar(currentDate);
    }

    public void NextMonth()
    {
        currentDate = currentDate.AddMonths(1);
        GenerateCalendar(currentDate);
    }

    void GenerateCalendar(DateTime targetDate)
    {
        // Clear previous buttons
        foreach (Transform child in calendarGrid)
        {
            Destroy(child.gameObject);
        }

        DateTime firstDay = new DateTime(targetDate.Year, targetDate.Month, 1);
        int daysInMonth = DateTime.DaysInMonth(targetDate.Year, targetDate.Month);
        int startDayOfWeek = (int)firstDay.DayOfWeek;

        monthYearText.text = targetDate.ToString("MMMM yyyy");

        // Padding for days before the 1st
        for (int i = 0; i < startDayOfWeek; i++)
        {
            Instantiate(dayButtonPrefab, calendarGrid); // empty placeholder
        }

        // Days of the month
        for (int day = 1; day <= daysInMonth; day++)
        {
            GameObject dayBtn = Instantiate(dayButtonPrefab, calendarGrid);
            dayBtn.GetComponentInChildren<Text>().text = day.ToString();

            // Optional: Add onClick behavior here
            int capturedDay = day;
            dayBtn.GetComponent<Button>().onClick.AddListener(() => OnDayClicked(capturedDay));
        }
    }

    void OnDayClicked(int day)
    {
        Debug.Log("Clicked on: " + new DateTime(currentDate.Year, currentDate.Month, day).ToShortDateString());
    }
}
