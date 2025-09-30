using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for MembershipUser
/// </summary>
public class MembershipUser
{
  public MembershipUser()
  {
    //
    // TODO: Add constructor logic here
    //
  }

  private string comment;

  public string Comment
  {
    get { return comment; }
    set { comment = value; }
  }

  private DateTime creationDate;

  public DateTime CreationDate
  {
    get { return creationDate; }
    set { creationDate = value; }
  }

  private string email;

  public string Email
  {
    get { return email; }
    set { email = value; }
  }

  private bool isApproved;

  public bool IsApproved
  {
    get { return isApproved; }
    set { isApproved = value; }
  }

  private bool isLockedOut;

  public bool IsLockedOut
  {
    get { return isLockedOut; }
    set { isLockedOut = value; }
  }

  private bool isOnline;

  public bool IsOnline
  {
    get { return isOnline; }
    set { isOnline = value; }
  }

  private DateTime lastActivityDate;

  public DateTime LastActivityDate
  {
    get { return lastActivityDate; }
    set { lastActivityDate = value; }
  }

  private DateTime lastLockoutDate;

  public DateTime LastLockoutDate
  {
    get { return lastLockoutDate; }
    set { lastLockoutDate = value; }
  }

  private DateTime lastLoginDate;

  public DateTime LastLoginDate
  {
    get { return lastLoginDate; }
    set { lastLoginDate = value; }
  }

  private DateTime lastPasswordChangedDate;

  public DateTime LastPasswordChangedDate
  {
    get { return lastPasswordChangedDate; }
    set { lastPasswordChangedDate = value; }
  }

  private string passwordQuestion;

  public string PasswordQuestion
  {
    get { return passwordQuestion; }
    set { passwordQuestion = value; }
  }

  private string providerName;

  public string ProviderName
  {
    get { return providerName; }
    set { providerName = value; }
  }

  private object providerUserKey;

  public object ProviderUserKey
  {
    get { return providerUserKey; }
    set { providerUserKey = value; }
  }

  private string userName;

  public string UserName
  {
    get { return userName; }
    set { userName = value; }
  }
}
