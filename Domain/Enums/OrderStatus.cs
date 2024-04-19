﻿namespace Domain.Enums;

public enum OrderStatus
{
    Pending      = 1,
    Processing   = 2,
    Shipped      = 3,
    Delivered    = 4,
    Cancelled    = 5,
    OnHold       = 6,
    Returned     = 7,
    Completed    = 8
}