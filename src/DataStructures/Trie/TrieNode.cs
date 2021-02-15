using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CsharpAlgorithmsAndDataStructures.DataStructures
{
    internal class TrieNode
    {
        private readonly char _character;
        private readonly Dictionary<char, TrieNode> _children = new();

        public bool IsCompleteWord { get; set; }

        public bool HasChildren => _children.Count > 0;

        public TrieNode(char character, bool isCompleteWord)
        {
            _character = character;
            IsCompleteWord = isCompleteWord;
        }

        public bool HasChild(char character) => _children.ContainsKey(character);

        public bool TryGetChild(char character, [NotNullWhen(true)]out TrieNode? child) => _children.TryGetValue(character, out child);

        public TrieNode AddChild(char character, bool isCompleteWord)
        {
            if (!_children.ContainsKey(character))
            {
                _children.Add(character, new TrieNode(character, isCompleteWord));
            }
            var childNode = _children[character];
            // In cases similar to adding "car" after "carpet" we need to mark "r" character as complete.
            childNode.IsCompleteWord = childNode.IsCompleteWord || isCompleteWord;
            return childNode;
        }

        public void RemoveChild(char character)
        {
            if (_children.TryGetValue(character, out var child) && IsIncompleteWithNoChildren(child))
            {
                _children.Remove(character);
            }

            static bool IsIncompleteWithNoChildren(TrieNode node)
                => !node.IsCompleteWord && !node.HasChildren;
        }

        public IReadOnlyCollection<char> SuggestChildren() => _children.Keys;

        public override string ToString()
            => $"{_character}{(IsCompleteWord ? "*" : "")}{(HasChildren ? ":" : "")}{(HasChildren ? string.Join(',', SuggestChildren()) : "")}";
    }
}