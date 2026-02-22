Imports System

Class Member

    Private _Username As String
    Private _Password As String
    Protected _UniqueID As String

    Public Property MemberUsername As String

        Get
            Return _Username
        End Get

        Set(ByVal value As String)
            _Username = value
        End Set

    End Property


    Public Property MemberPassword As String

        Get
            Return _Password
        End Get

        Set(ByVal value As String)
            _Password = value
        End Set

    End Property

    Public ReadOnly Property MemberUniqueID As String

        Get
            Return _UniqueID
        End Get

    End Property

    Public Overridable Sub Entry(ByVal Lanyard As Lanyard)

        Console.WriteLine("Welcome to the building " & _Username)

    End Sub

    Public Sub New(ByVal MemberNumber As Integer)

        Console.WriteLine("What is their Username?")
        _Username = Console.ReadLine()
        Console.WriteLine("what is their Password?")
        _Password = Console.ReadLine()
        _UniqueID = MemberNumber

    End Sub

End Class



Class Lanyard

    Private _Owner As Member
    Private _MemberID As String

    Public Sub New(ByVal Member As Member)

        _Owner = Member
        _MemberID = Member.MemberUniqueID

    End Sub

End Class

Class Adult

    Private DBS As Boolean = True

End Class

Class Child

    Private _MinimumGradeRequirement As Boolean = True
    Private _Allergies As New List(Of String)

    Public Sub New()
        AddAllergies()
    End Sub

    Public Sub AddAllergies()

        Console.WriteLine("Does this child have any allergies?")
        Dim allergy As String = Console.ReadLine()

        If allergy.ToLower = "yes" Then

            Dim done As Boolean = False

            Do Until done = True

                Console.WriteLine("Please enter the allergy and press enter to submit")
                allergy = Console.ReadLine()
                _Allergies.Add(allergy)

                Console.WriteLine("Any more?")

                Dim response As String = Console.ReadLine()

                If response.ToLower = "no" Then
                    done = True
                End If

            Loop

        Else

            Console.WriteLine("Lucky you!")

        End If

    End Sub

End Class

Class Teacher

    Protected _TeacherMember As Member 'Member contains information about teacher's username, password, uniqueID
    Private _TeacherAdult As Adult 'Adult contains information about teacher's DBS check
    Private _Department As String
    Private _YearsOfExperience As Integer
    Protected _Salary As Integer
    Private _ClassA As New List(Of Student)
    Private _ClassB As New List(Of Student)
    Private _ClassC As New List(Of Student)
    Private _ClassD As New List(Of Student)

    Public Sub New(ByVal MemberNumber As Integer)

        _TeacherMember = New Member(MemberNumber) 'Member and Adult both CREATED inside Teacher --> Composition 
        _TeacherAdult = New Adult() 'Member and Adult both CREATED inside Teacher --> Composition

        Console.WriteLine("Please enter " & Me._TeacherMember.MemberUsername & "'s department.")
        _Department = Console.ReadLine()
        Console.WriteLine("Please enter " & Me._TeacherMember.MemberUsername & "'s years of experience.")
        _YearsOfExperience = Console.ReadLine()
        _Salary = 30000 + (_YearsOfExperience * 1000)

        DisplayDetails()

    End Sub

    Public Sub DisplayDetails()

        Console.WriteLine("Username: " & Me._TeacherMember.MemberUsername)
        Console.WriteLine("Password: PROTECTED")
        Console.WriteLine("UniqueID: " & Me._TeacherMember.MemberUniqueID)
        Console.WriteLine("Department: " & _Department)
        Console.WriteLine("Years of Experience: " & _YearsOfExperience)
        Console.WriteLine("Salary: £" & _Salary)
    End Sub

    Public Sub AddStudentToClass(ByVal StudentToAdd As Student) 'Aggregation of Student in Teacher class as a teacher can have multiple students but a student can exist without a teacher

        While True

            Console.WriteLine("Which class do you want to add " & StudentToAdd.StudentUsername & " to? (A, B, C or D)")

            Dim choice As String = Console.ReadLine()

            Select Case choice.ToUpper
                Case "A"
                    _ClassA.Add(StudentToAdd)
                    Exit While
                Case "B"
                    _ClassB.Add(StudentToAdd)
                    Exit While
                Case "C"
                    _ClassC.Add(StudentToAdd)
                    Exit While
                Case "D"
                    _ClassD.Add(StudentToAdd)
                    Exit While
                Case Else
                    Console.WriteLine("Invalid class choice.")
            End Select

        End While

        Console.WriteLine($"{StudentToAdd.StudentUsername} has been added to class you have chosen.")

    End Sub

End Class

Class SeniorTeacher

    Inherits Teacher 'SeniorTeacher is a type of Teacher --> Inheritance
    Private _Role As String
    Private _Team As New List(Of Teacher)

    Public Sub New(ByVal MemberNumber As Integer)

        MyBase.New(MemberNumber) 'Calls the constructor of the Teacher class to create the Member and Adult objects
        Console.WriteLine("Please enter " & Me._TeacherMember.MemberUsername & "'s role.")
        _Role = Console.ReadLine()

        If _Role.ToLower = "principal" Then
            Me._Salary *= 3
        ElseIf _Role.ToLower = "deputy principal" Then
            Me._Salary *= 2
        ElseIf _Role.ToLower = "head of department" Then
            Me._Salary *= 1.5

        End If

        DisplayDetails()
    End Sub

    Public Function AddTeamMember(ByVal TeacherToAdd As Teacher) As String 'Aggregation

        If _Team.Count < 5 Then
            _Team.Add(TeacherToAdd)
            Return True
        Else Console.WriteLine("Team is full I'm afraid!")
            Return False
        End If
    End Function

End Class

Class Student 'Composite of Member and Child 

    Private _StudentMember As Member 'Member contains information about student's username, password, uniqueID 
    Private _StudentChild As Child 'Child contains information about student's allergies 
    Private _Subjects(3) As String

    Public ReadOnly Property StudentUsername As String

        Get
            Return _StudentMember.MemberUsername
        End Get

    End Property

    Public ReadOnly Property StudentSubjects() As String

        Get
            For i As Integer = 1 To 3
                Console.WriteLine(_Subjects(i))
            Next
        End Get

    End Property

    Public Sub New(ByVal MemberNumber As Integer)

        _StudentMember = New Member(MemberNumber) 'Member and Child both CREATED inside Student --> Composition 
        _StudentChild = New Child() 'Member and Child both CREATED inside Student --> Composition 

        Console.WriteLine("Please enter " & Me._StudentMember.MemberUsername & "'s three subjects one by one.")

        _Subjects(1) = Console.ReadLine()
        _Subjects(2) = Console.ReadLine()
        _Subjects(3) = Console.ReadLine()

        Console.WriteLine(Me._StudentMember.MemberUsername & " has been successfully added. Welcome to Sir John Deane's!")
        DisplayDetails()

    End Sub

    Public Sub DisplayDetails()

        Console.WriteLine("Username: " & Me._StudentMember.MemberUsername)
        Console.WriteLine("Password: PROTECTED")
        Console.WriteLine("UniqueID: " & Me._StudentMember.MemberUniqueID)

        For i As Integer = 1 To 3
            Console.WriteLine("Subject" & i & ": " & _Subjects(i))
        Next

    End Sub

End Class

Module Module1

    Sub Main()

        Dim Students(2000) As Student
        Dim Teachers(70) As Teacher

        Console.WriteLine("Welcome to the SJD Community Program")

        Dim finished As Boolean = False
        Dim studentCount As Integer = 0
        Dim teacherCount As Integer = 0

        Do Until finished = True

            Console.WriteLine("Do you wish to add a Teacher or Student?")

            Dim choice As String = Console.ReadLine()

            If choice.ToLower = "student" Then

                Dim NewStudent As New Student(studentCount)

                studentCount += 1

                Students(studentCount) = NewStudent 'Saves student into the correct array index 

            Else

                Console.WriteLine("Do you wish to add a Senior Teacher or not (just regular) ?")

                Dim choice2 As String = Console.ReadLine()

                If choice2.ToLower = "senior" Then
                    Dim NewSeniorTeacher As New SeniorTeacher(teacherCount)
                    teacherCount += 1
                    Teachers(teacherCount) = NewSeniorTeacher
                Else
                    Dim NewTeacher As New Teacher(teacherCount)
                    teacherCount += 1
                    Teachers(teacherCount) = NewTeacher
                End If


            End If

            Console.WriteLine("Do you wish to add another member?")

            Dim response As String = Console.ReadLine()

            If response.ToLower = "no" Then

                Console.WriteLine("OK thank you for using the SJD Community Program")

                finished = True

                System.Threading.Thread.Sleep(1000)

            End If

        Loop

    End Sub



End Module




