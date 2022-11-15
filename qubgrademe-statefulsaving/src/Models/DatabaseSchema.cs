using System;

namespace qubgrademe_statefulsaving;

public class DatabaseSchema
{
    public int Id { get; set; }
    public Guid SessionID { get; set; }
    public string Data { get; set; }
}