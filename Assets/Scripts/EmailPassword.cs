using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class EmailPassword : MonoBehaviour
{

    public GameObject signUpUI;
    public GameObject loggedInUI;


    private FirebaseAuth auth;
    public InputField NameInput, UserNameInput;
    string PasswordInput;
    public Button SignupButton;
    // public Button LoginButton;
    public Text ErrorText;
    public Text LoginResultText;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        //Just an example to save typing in the login form
        PlayerPrefs.SetString("U_PASS", "NooraHealth");

        var u_name = PlayerPrefs.GetString("U_NAME");
        var u_email = PlayerPrefs.GetString("U_EMAIL");
        var u_pass = PlayerPrefs.GetString("U_PASS");

        if (u_email != null && u_pass != null && u_name != null)
        {
            NameInput.text = u_name;
            UserNameInput.text = u_email;
            PasswordInput = u_pass;

            // Login User Automatically
            Login(u_email, u_pass);
        }

        SignupButton.onClick.AddListener(() => Signup(UserNameInput.text, PasswordInput));
        // LoginButton.onClick.AddListener(() => Login(UserNameInput.text, PasswordInput));
    }


    public void Signup(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            //Error handling
            return;
        }

        PlayerPrefs.SetString("U_EMAIL", email);
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync error: " + task.Exception);
                if (task.Exception.InnerExceptions.Count > 0)
                    UpdateErrorMessage(task.Exception.InnerExceptions[0].Message);
                return;
            }

            FirebaseUser newUser = task.Result; // Firebase user has been created.
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            UpdateErrorMessage("Signup Success");
            // Update User's Name
            UpdateName();
            // LogIn User
            Debug.Log(PlayerPrefs.GetString("U_EMAIL") + "  " + PlayerPrefs.GetString("U_PASS"));
            Login(PlayerPrefs.GetString("U_EMAIL"), PlayerPrefs.GetString("U_PASS"));
        });
    }

    public void UpdateName()
    {
        //Update User Name

        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
            {
                DisplayName = NameInput.text,
                PhotoUrl = new System.Uri("https://example.com/jane-q-user/profile.jpg"),
            };
            user.UpdateUserProfileAsync(profile).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("User profile updated successfully.");

            });
        }

    }
    private void UpdateErrorMessage(string message)
    {
        ErrorText.text = message;
        Invoke("ClearErrorMessage", 3);
    }

    void ClearErrorMessage()
    {
        ErrorText.text = "";
    }
    public void Login(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync error: " + task.Exception);
                if (task.Exception.InnerExceptions.Count > 0)
                    UpdateErrorMessage(task.Exception.InnerExceptions[0].Message);
                return;
            }

            FirebaseUser user = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                user.DisplayName, user.UserId);

            PlayerPrefs.SetString("U_EMAIL", user != null ? user.Email : "Unknown");
            PlayerPrefs.SetString("U_NAME", user != null ? user.DisplayName : "Unknown");
            PlayerPrefs.SetString("U_PASS", user != null ? PasswordInput : "Unknown");
            PlayerPrefs.SetString("U_ID", user != null ? user.UserId : "Unknown");
            // SceneManager.LoadScene("LoginResults");

            signUpUI.SetActive(false);
            loggedInUI.SetActive(true);

            // Log In Message
            var userName = PlayerPrefs.GetString("U_NAME", "Unknown");
            LoginResultText.text = "Welcome " + userName + " to GoCorona Game!!";
            Debug.LogFormat("Successfully signed in as {0}", userName);
        });
    }
}