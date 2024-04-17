﻿using Application.Abstractions.Messaging;
using Domain.Entities;

namespace Application.Orders.CreateOrder;

public sealed record CreateOrderCommand(
                            HashSet<Guid> ProductIds) : ICommand<Guid>;