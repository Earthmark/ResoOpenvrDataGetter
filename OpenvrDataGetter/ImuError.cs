using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenvrDataGetter;

public enum ImuErrorCode
{
    None = 0, //IOBuffer_Success = 0,
    AlreadyOpened,
    AlreadyClosed,
    UnknownException,
    PathIsNullOrEmpty,
    OpenVrNotFound,
    IOBuffer_OperationFailed = 100,
    IOBuffer_InvalidHandle = 101,
    IOBuffer_InvalidArgument = 102,
    IOBuffer_PathExists = 103,
    IOBuffer_PathDoesNotExist = 104,
    IOBuffer_Permission = 105
}
