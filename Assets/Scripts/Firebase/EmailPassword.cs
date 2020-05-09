using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Security.Cryptography;
using System.Text;

public class EmailPassword : MonoBehaviour
{

    // public GameObject signUpUI;
    // public GameObject loggedInUI;

    private FirebaseAuth auth;
    public InputField NameInput;
    string PasswordInput, UserNameInput;
    public Button SignupButton;
    // public Button LoginButton;
    public Text ErrorText;
    public Text LoginResultText;

    internal static readonly char[] chars =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();


    // Start Method
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        //Just an example to save typing in the login form
        PlayerPrefs.SetString("U_PASS", "GoCorona");
        
        var u_name = PlayerPrefs.GetString("U_NAME");

        var u_email = PlayerPrefs.GetString("U_EMAIL");

        Debug.Log("Current Email ID = " + u_email);
        // If Email is null, Assign a new Email
        if(u_email== "")
        {
            System.Random size = new System.Random();
            string randomEmail = GetUniqueKey(size.Next(8, 16)) + "@gocorona.com";
            PlayerPrefs.SetString("U_EMAIL", randomEmail);
            Debug.Log("New email created: " + randomEmail);
        }

        u_email = PlayerPrefs.GetString("U_EMAIL");

        var u_pass = PlayerPrefs.GetString("U_PASS");

        if (u_email != null && u_pass != null && u_name != null && u_name != " ")
        {
            NameInput.text = u_name;
            UserNameInput = u_email;
            PasswordInput = u_pass;

            // Login User Automatically
            Login(u_email, u_pass);
        }

        SignupButton.onClick.AddListener(() => Signup(UserNameInput, PasswordInput, NameInput.text));
        // LoginButton.onClick.AddListener(() => Login(UserNameInput, PasswordInput));
    }

    // Sign Up user in Database
    public void Signup(string email, string password, string name)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(name))
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


            PlayerPrefs.SetString("U_EMAIL", newUser != null ? newUser.Email : "Unknown");
            PlayerPrefs.SetString("U_PASS", newUser != null ? PasswordInput : "Unknown");
            PlayerPrefs.SetString("U_NAME", newUser != null ? newUser.DisplayName : "Unknown");
            PlayerPrefs.SetString("U_ID", newUser != null ? newUser.UserId : "Unknown");
            
            //Set Default Score to 0
            PlayerPrefs.SetInt("U_SCORE", 0);

            // Update User's Name
            UpdateName();

            Debug.Log("--------------Creds------------------ \n" + PlayerPrefs.GetString("U_EMAIL") + "  " + PlayerPrefs.GetString("U_PASS") + "  " + PlayerPrefs.GetString("U_NAME") + "  " + PlayerPrefs.GetString("U_ID"));

            // LogIn User
            Login(PlayerPrefs.GetString("U_EMAIL"), PlayerPrefs.GetString("U_PASS"));
        });
    }

    // Update User's Name in Database
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

    // Update Error Message
    private void UpdateErrorMessage(string message)
    {
        ErrorText.text = message;
        Invoke("ClearErrorMessage", 3);
    }

    // Clear Error Message
    void ClearErrorMessage()
    {
        ErrorText.text = "";
    }

    // Login User
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

            SceneManager.LoadScene("00 Start");

            PlayerPrefs.SetString("U_NAME", user != null ? user.DisplayName : "Unknown");
            // SceneManager.LoadScene("LoginResults");
            Debug.Log("-------------Creds------------------- \n" + PlayerPrefs.GetString("U_EMAIL") + "  " + PlayerPrefs.GetString("U_PASS") + "  " + PlayerPrefs.GetString("U_NAME") + "  " + PlayerPrefs.GetString("U_ID"));
            // signUpUI.SetActive(false);
            // loggedInUI.SetActive(true);

            // Log In Message
            var userName = PlayerPrefs.GetString("U_NAME");
            LoginResultText.text = "Hi! " + userName;
            Debug.LogFormat("Successfully signed in as {0}", userName);
        });
    }


    // Generate Random String
    public static string GetUniqueKey(int size)
    {
        byte[] data = new byte[4 * size];
        using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
        {
            crypto.GetBytes(data);
        }
        StringBuilder result = new StringBuilder(size);
        for (int i = 0; i < size; i++)
        {
            var rnd = BitConverter.ToUInt32(data, i * 4);
            var idx = rnd % chars.Length;

            result.Append(chars[idx]);
        }

        return result.ToString();
    }
}