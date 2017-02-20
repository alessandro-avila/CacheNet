﻿Imports CacheManager.Core

Public Class CacheManagerProvider
    Inherits CacheProviderBase(Of ICacheManager(Of Object))
    Protected Overrides Function InitCache() As ICacheManager(Of Object)
        Return CacheFactory.FromConfiguration(Of Object)("Redis")
    End Function

    Public Overrides Function GetValue(Of T)(key As String) As T
        Return Cache.[Get](Of T)(KeyPrefix & key)
    End Function

    Public Overrides Sub SetValue(Of T)(key As String, value As T)
        Cache.Add(KeyPrefix & key, value)
        Cache.Expire(KeyPrefix & key, DateTime.Now.AddMinutes(CacheDuration))
    End Sub

    Public Overrides Sub SetSliding(Of T)(key As String, value As T)
        Cache.Add(KeyPrefix & key, value)
        Cache.Expire(KeyPrefix & key, New TimeSpan(0, CacheDuration, 0))
    End Sub

    Public Overrides Sub SetValue(Of T)(key As String, value As T, duration As Integer)
        Cache.Add(KeyPrefix & key, value)
        Cache.Expire(KeyPrefix & key, DateTime.Now.AddMinutes(duration))
    End Sub

    Public Overrides Sub SetSliding(Of T)(key As String, value As T, duration As Integer)
        Cache.Add(KeyPrefix & key, value)
        Cache.Expire(KeyPrefix & key, New TimeSpan(0, duration, 0))
    End Sub

    Public Overrides Sub SetValue(Of T)(key As String, value As T, expiration As DateTimeOffset)
        Cache.Add(KeyPrefix & key, value)
        Cache.Expire(KeyPrefix & key, expiration)
    End Sub

    Public Overrides Function Exists(key As String) As Boolean
        Return Cache.Exists(key)
    End Function

    Public Overrides Sub Remove(key As String)
        Cache.Remove(KeyPrefix & key)
    End Sub
End Class