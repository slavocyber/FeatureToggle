namespace Feature.Client.Common;

internal enum ErrorCode : ushort
{
    None = 0,
    JsonUpdated = 1,
    SameFeat = 2,
    FeatIsNotFound = 3
}
