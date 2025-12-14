using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DeckWizard : EditorWindow
{
    private List<Deck> _decks;
    private List<Deck> _filteredDecks;
    private List<Card> _allCards;
    private List<Card> _filteredCards;
    
    private string _decksFilter = "";
    private string _cardsFilter = "";
    private Vector2 _decksScrollPosition;
    private Vector2 _cardsInDeckScrollPosition;
    private Vector2 _cardsScrollPosition;

    private Deck _selectedDeck;
    private Dictionary<Card, int> _selectedDeckCards;
    private Card _selectedCard;

    private GUIStyle _deckButtonStyle;
    
    private float ArrowsColumnWidth => 100;
    private float ColumnWidth => (Screen.width - 36 - ArrowsColumnWidth) / 3f;

    private const float COLUMNS_SPACING = 8; 
    
    [MenuItem("EditorScripting/DeckWizard")]
    public static void OpenWindow()
    {
        var window = GetWindow<DeckWizard>(true, "DeckWizard");
        window.minSize = new Vector2(900, 400);
    }

    private void OnFocus()
    {
        FetchAssets(ref _decks);
        _filteredDecks = _decks;
        
        FetchAssets(ref _allCards);
        _filteredCards = _allCards;
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        DrawFilteredDeckList();
        GUILayout.Space(COLUMNS_SPACING);
        DrawDeckCards();
        GUILayout.Space(COLUMNS_SPACING);
        DrawControlArrows();
        GUILayout.Space(COLUMNS_SPACING);
        DrawAllCards();
        GUILayout.EndHorizontal();
    }

    private void FetchAssets<T>(ref List<T> assets) where T : ScriptableObject
    {
        assets = new List<T>(); 
        var assetsGuids = AssetDatabase.FindAssets($"t:{typeof(T)}");
        foreach (var assetGuid in assetsGuids)
        {
            var path = AssetDatabase.GUIDToAssetPath(assetGuid);
            var asset = AssetDatabase.LoadAssetAtPath<T>(path);
            assets.Add(asset);
        }
    }

    private void DrawFilteredDeckList()
    {
        GUILayout.BeginVertical(GUILayout.Width(ColumnWidth), GUILayout.ExpandHeight(true));
        GUILayout.Label("Decks");
        var prevFilter = _decksFilter;
        DrawFilter(ref _decksFilter, "Deck filter");
        if (_decksFilter != prevFilter)
        {
            FilterDecks();
        }
        DrawDeckList();
        GUILayout.EndVertical();
    }

    private void FilterDecks()
    {
        _filteredDecks = new List<Deck>();
        foreach (var deck in _decks)
        {
            if (deck.Name.ToLower().Replace(" ", "").Contains(
                    _decksFilter.ToLower().Replace(" ", "")))
            {
                _filteredDecks.Add(deck);
            }
        }
    }
    
    private void DrawDeckList()
    {
        _decksScrollPosition = GUILayout.BeginScrollView(_decksScrollPosition, false, true);

        foreach (var deck in _filteredDecks)
        {
            DrawDeckOption(deck);
        }
        
        GUILayout.EndScrollView();
    }

    private void DrawDeckOption(Deck deck)
    {
        var prevBackground = GUI.backgroundColor;
        var isThisDeckSelected = deck == _selectedDeck;
        if (isThisDeckSelected)
        {
            GUI.backgroundColor = Color.yellow;  
        }
        
        _deckButtonStyle ??= new GUIStyle(GUI.skin.button) { alignment = TextAnchor.MiddleLeft };
        if (GUILayout.Button(deck.Name, _deckButtonStyle))
        {
            _selectedDeck = isThisDeckSelected ? null : deck; 
            // if (isThisDeckSelected) 
            // {
            //     _selectedDeck = null;
            // }
            // else
            // {
            //     _selectedDeck = deck;
            // }

            _selectedDeckCards = _selectedDeck == null ? new Dictionary<Card, int>() : _selectedDeck.GetGroupedCards();
            // if (_selectedDeck == null)
            // {
            //     _selectedDeckCards = new Dictionary<Card, int>();
            // }
            // else
            // {
            //     _selectedDeckCards = _selectedDeck.GetGroupedCards();
            // }
        }
        
        GUI.backgroundColor = prevBackground;  
    }
    
    private void DrawDeckCards()
    {
        GUILayout.BeginVertical(GUILayout.Width(ColumnWidth), GUILayout.ExpandHeight(true));
        
        GUILayout.Label("Selected Deck");
        
        _cardsInDeckScrollPosition = GUILayout.BeginScrollView(_cardsInDeckScrollPosition, 
            false, true);
        if (_selectedDeck == null)
        {
            var prevColor = GUI.contentColor;
            GUI.contentColor = Color.yellow;
            GUILayout.Label("Select a Deck to edit");
            GUI.contentColor = prevColor;
        }
        else
        {
            DrawSelectedDeckCards();
        }
        
        GUILayout.EndScrollView();
        
        GUILayout.EndVertical();
    }

    private void DrawSelectedDeckCards()
    {
        _selectedDeckCards ??= new Dictionary<Card, int>();
        // if (_selectedDeckCards == null)
        // {
        //     _selectedDeckCards = new Dictionary<Card, int>();
        // }

        if (_selectedDeckCards.Count == 0)
        {
            _selectedDeckCards = _selectedDeck.GetGroupedCards();
        }
        
        foreach (var pair in _selectedDeckCards)
        {
            DrawCardOption(pair.Key, pair.Value);
        }
    }

    private void DrawCardOption(Card card, int amount, bool drawAmount = true)
    {
        GUILayout.BeginHorizontal();

        var prevBackground = GUI.backgroundColor;
        var isThisCardSelected = card == _selectedCard;
        if (isThisCardSelected)
        {
            GUI.backgroundColor = Color.yellow;
        }

        if (GUILayout.Button(card.Name, _deckButtonStyle))
        {
            _selectedCard = isThisCardSelected ? null : card;
        }

        GUI.backgroundColor = prevBackground;

        GUILayout.BeginHorizontal(GUILayout.Width(60));

        GUILayout.Box(drawAmount ? $"x{amount}" : "",
            GUILayout.Width(30), GUILayout.Height(EditorGUIUtility.singleLineHeight));
        GUILayout.Space(2);
        GUILayout.Box(card.Cost.ToString(),
            GUILayout.Width(30), GUILayout.Height(EditorGUIUtility.singleLineHeight));

        GUILayout.EndHorizontal();

        GUILayout.EndHorizontal();
    }

    private void DrawControlArrows()
    {
        GUILayout.BeginVertical(GUILayout.Width(ArrowsColumnWidth), GUILayout.ExpandHeight(true));
        GUILayout.Label("Arrows");
        GUILayout.EndVertical();
    }
    
    private void DrawAllCards()
    {
        GUILayout.BeginVertical(GUILayout.Width(ColumnWidth), GUILayout.ExpandHeight(true));
        
        GUILayout.Label("All cards");
        
        var prevFilter = _cardsFilter;
        DrawFilter(ref _cardsFilter, "Cards filter");
        if (_cardsFilter != prevFilter)
        {
            FilterCards();
        }
        
        _cardsScrollPosition = GUILayout.BeginScrollView(_cardsScrollPosition, 
            false, true);
        foreach (var card in _filteredCards)
        {
            DrawCardOption(card, 0, false);
        }

        GUILayout.EndScrollView();
        
        GUILayout.EndVertical();
    }
    
    private void FilterCards()
    {
        _filteredCards = new List<Card>();
        foreach (var card in _allCards)
        {
            if (card.Name.ToLower().Replace(" ", "").Contains(
                    _cardsFilter.ToLower().Replace(" ", "")))
            {
                _filteredCards.Add(card);
            }
        }
    }
    
    private void DrawFilter(ref string currentFilter, string placeholder)
    {
        var prevContentColor = GUI.contentColor;
        GUI.contentColor = currentFilter == "" ? Color.gray : prevContentColor;
        currentFilter = EditorGUILayout.TextField(currentFilter == "" ? placeholder : currentFilter);
        GUI.contentColor = prevContentColor;
        currentFilter = currentFilter == placeholder ? "" : currentFilter;
    }
}
