// Generated by TinyPG v1.3 available at www.codeproject.com

using System;
using System.Collections.Generic;

namespace MeisterGeister.Logic.Würfel
{
    #region Parser

    public partial class Parser 
    {
        private Scanner scanner;
        private ParseTree tree;
        
        public Parser(Scanner scanner)
        {
            this.scanner = scanner;
        }

        public ParseTree Parse(string input)
        {
            tree = new ParseTree();
            return Parse(input, tree);
        }

        public ParseTree Parse(string input, ParseTree tree)
        {
            scanner.Init(input);

            this.tree = tree;
            ParseStart(tree);
            tree.Skipped = scanner.Skipped;

            return tree;
        }

        private void ParseStart(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Start), "Start");
            parent.Nodes.Add(node);


            
            tok = scanner.LookAhead(TokenType.NUMBER, TokenType.W, TokenType.PLUSMINUS);
            if (tok.Type == TokenType.NUMBER
                || tok.Type == TokenType.W
                || tok.Type == TokenType.PLUSMINUS)
            {
                ParseAddExpression(node);
            }

            
            tok = scanner.Scan(TokenType.EOF);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.EOF) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.EOF.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                return;
            }

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseAddExpression(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.AddExpression), "AddExpression");
            parent.Nodes.Add(node);


            
            ParseAtom(node);

            
            tok = scanner.LookAhead(TokenType.PLUSMINUS);
            while (tok.Type == TokenType.PLUSMINUS)
            {

                
                tok = scanner.Scan(TokenType.PLUSMINUS);
                n = node.CreateNode(tok, tok.ToString() );
                node.Token.UpdateRange(tok);
                node.Nodes.Add(n);
                if (tok.Type != TokenType.PLUSMINUS) {
                    tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.PLUSMINUS.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                    return;
                }

                
                ParseAtom(node);
            tok = scanner.LookAhead(TokenType.PLUSMINUS);
            }

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseAtom(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Atom), "Atom");
            parent.Nodes.Add(node);


            
            tok = scanner.LookAhead(TokenType.NUMBER);
            if (tok.Type == TokenType.NUMBER)
            {
                tok = scanner.Scan(TokenType.NUMBER);
                n = node.CreateNode(tok, tok.ToString() );
                node.Token.UpdateRange(tok);
                node.Nodes.Add(n);
                if (tok.Type != TokenType.NUMBER) {
                    tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.NUMBER.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                    return;
                }
            }

            
            tok = scanner.LookAhead(TokenType.W);
            if (tok.Type == TokenType.W)
            {

                
                tok = scanner.Scan(TokenType.W);
                n = node.CreateNode(tok, tok.ToString() );
                node.Token.UpdateRange(tok);
                node.Nodes.Add(n);
                if (tok.Type != TokenType.W) {
                    tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.W.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                    return;
                }

                
                tok = scanner.LookAhead(TokenType.NUMBER2);
                if (tok.Type == TokenType.NUMBER2)
                {
                    tok = scanner.Scan(TokenType.NUMBER2);
                    n = node.CreateNode(tok, tok.ToString() );
                    node.Token.UpdateRange(tok);
                    node.Nodes.Add(n);
                    if (tok.Type != TokenType.NUMBER2) {
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.NUMBER2.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                        return;
                    }
                }
            }

            parent.Token.UpdateRange(node.Token);
        }


    }

    #endregion Parser
}
