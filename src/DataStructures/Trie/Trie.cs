using System.Collections.Generic;
using System.Linq;

namespace CsharpAlgorithmsAndDataStructures.DataStructures.Trie
{
    public class Trie
    {
        private readonly TrieNode _head;
        private const char HeadCharacter = '*';

        public Trie() => _head = new TrieNode(HeadCharacter, false);
        
        public Trie AddWord(string word) {
            var currentNode = _head;
            foreach (var current in word)
            {
                currentNode = currentNode.AddChild(current, false);
            }
            currentNode.IsCompleteWord = true;
            return this;
        }
        

        public Trie DeleteWord(string word) {
            void DepthFirstDelete(TrieNode currentNode, int charIndex = 0){
                if (charIndex >= word.Length) {
                    // Return if we're trying to delete the character that is out of word's scope.
                    return;
                }

                var character = word[charIndex];
                if (!currentNode.TryGetChild(character, out var nextNode))
                {
                    // Return if we're trying to delete a word that has not been added to the Trie.
                    return;
                }

                // Go deeper.
                DepthFirstDelete(nextNode, charIndex + 1);

                // Since we're going to delete a word let's un-mark its last character isCompleteWord flag.
                if (charIndex == (word.Length - 1)) {
                    nextNode.IsCompleteWord = false;
                }

                // childNode is deleted only if:
                // - childNode has NO children
                // - childNode.isCompleteWord == false
                currentNode.RemoveChild(character);
            };

            // Start depth-first deletion from the head node.
            DepthFirstDelete(_head);
            return this;
        }
        
        public IReadOnlyCollection<char>? SuggestNextCharacters(string word) 
            => GetLastCharacterNode(word)?.SuggestChildren() ?? null;

        public bool DoesWordExist(string word) => GetLastCharacterNode(word)?.IsCompleteWord ?? false;

        public TrieNode? GetLastCharacterNode(string word) 
        {
            var currentNode = _head;
            return word.All(character => currentNode.TryGetChild(character, out currentNode))
                ? currentNode
                : null;
        }
    }
}