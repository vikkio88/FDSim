﻿namespace FDSim.Models.People;

using FDSim.Models.Common;
using FDSim.Models.Enums;
public class Player : Person
{
    public string ShortName { get => $"{Name.Substring(0, 1)}. {Surname}"; }

    public PlayerStatus Status { get; set; }
    public Role Role { get; init; }

    public Money Value { get; set; }
    public Player() : base() { Role = Role.Goalkeeper; Status = new PlayerStatus(); }
    public Player(String name, String surname, int age, Role role, PlayerStatus? status = null)
    : base(name, surname, age)
    {
        Role = role;
        Status = status ?? new PlayerStatus();
    }
    public Player(String name, String surname, int age, int skillAvg, Role role, PlayerStatus? status = null)
    : base(name, surname, age)
    {
        Skill = new(skillAvg);
        Role = role;
        Status = status ?? new PlayerStatus();
    }

    public override string ToString()
    {
        return String.Format("{0} - Player ({1})", base.ToString(), Role);
    }
}
