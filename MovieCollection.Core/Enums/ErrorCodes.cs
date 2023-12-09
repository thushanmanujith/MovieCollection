using MovieCollection.Core.Extensions;
using System.ComponentModel;
using System.Net;

namespace MovieCollection.Core.Enums
{
    public enum ErrorCodes
    {
        #region Account
        /// <summary>
        /// 400
        /// </summary>
        [Description("Invalid user registration details")]
        [HttpStatusCode(HttpStatusCode.BadRequest)]
        InvalidUserRegistrationDetails = 100100,

        [Description("Invalid password")]
        [HttpStatusCode(HttpStatusCode.BadRequest)]
        InvaildPassword = 100101,

        [Description("User already exist in the system")]
        [HttpStatusCode(HttpStatusCode.BadRequest)]
        UserAlreadyExist = 100102,

        [Description("Invalid email")]
        [HttpStatusCode(HttpStatusCode.BadRequest)]
        InvaildEmail = 100103,

        [Description("Invalid login request")]
        [HttpStatusCode(HttpStatusCode.BadRequest)]
        InvalidLoginRequest = 100104,

        [Description("Invalid movie collection")]
        [HttpStatusCode(HttpStatusCode.BadRequest)]
        InvalidCollection = 100105,

        [Description("Movie already exist in the system")]
        [HttpStatusCode(HttpStatusCode.BadRequest)]
        MovieAlreadyExist = 100106,

        [Description("Movie already exist in the system")]
        [HttpStatusCode(HttpStatusCode.BadRequest)]
        MoveCollectionAlreadyExist = 100107,

        /// <summary>
        /// 404
        /// </summary>
        [Description("User not found")]
        [HttpStatusCode(HttpStatusCode.NotFound)]
        UserNotFound = 100200,

        [Description("Move not found")]
        [HttpStatusCode(HttpStatusCode.NotFound)]
        MoveNotFound = 100201,

        [Description("Collection not found")]
        [HttpStatusCode(HttpStatusCode.NotFound)]
        CollectionNotFound = 100202,

        #endregion
    }
}
