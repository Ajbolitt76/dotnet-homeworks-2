﻿namespace Hw9.Parser.Tokens;

public class TokenTypeOperatorAdd : TokenTypeOperator
{
    public override string StringRepresentation => "+";
    
    public override OperatorKind OperatorKind => OperatorKind.Binary | OperatorKind.Unary;
}