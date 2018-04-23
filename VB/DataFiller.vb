Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.XtraScheduler

Namespace DayHeaderStatistics
	Public Class DataFiller
		Public Shared Users() As String = { "Sarah Brighton", "Ryan Fischer", "Andrew Miller" }
		Public Shared Usernames() As String = { "sbrighton", "rfischer", "amiller" }
		Public Shared RandomInstance As New Random()


		Public Shared Sub FillResources(ByVal storage As SchedulerStorage, ByVal count As Integer)
			Dim resources As ResourceCollection = storage.Resources.Items
			storage.BeginUpdate()
			Try
				Dim cnt As Integer = Math.Min(count, Users.Length)
				For i As Integer = 1 To cnt
					resources.Add(New Resource(Usernames(i - 1), Users(i - 1)))
				Next i
			Finally
				storage.EndUpdate()
			End Try
		End Sub


		Public Shared Sub GenerateAppointments(ByVal storage As SchedulerStorage)
			Dim count As Integer = storage.Resources.Count
			For i As Integer = 0 To count - 1
				Dim resource As Resource = storage.Resources(i)
				Dim subjPrefix As String = resource.Caption & "'s "

				storage.Appointments.Add (AptCreate(resource.Id, subjPrefix & "meeting", 2, 5))
				storage.Appointments.Add (AptCreate(resource.Id, subjPrefix & "travel", 3, 6))
				storage.Appointments.Add (AptCreate(resource.Id, subjPrefix & "phone call", 0, 10))
			Next i
		End Sub
		Public Shared Function AptCreate(ByVal resourceId As Object, ByVal subject As String, ByVal status As Integer, ByVal label As Integer) As Appointment
			Dim apt As New Appointment()
			apt.Subject = subject
			apt.ResourceId = resourceId
			Dim rnd As Random = RandomInstance
			Dim rangeInHours As Integer = 48
			apt.Start = DateTime.Today + TimeSpan.FromHours(rnd.Next(0, rangeInHours))
			apt.End = apt.Start + TimeSpan.FromHours(rnd.Next(0, rangeInHours \ 8))
			apt.StatusId = status
			apt.LabelId = label
			Return apt
		End Function

	End Class
End Namespace
