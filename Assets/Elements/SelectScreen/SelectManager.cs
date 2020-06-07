using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaisa.DigimonCrush.Misc {
    public class SelectManager : MonoBehaviour {
        [SerializeField] private SelectionCharacter[] chars;
        [SerializeField] private GameObject grid;

        private GameObject[] gridChars;
        private int _sel0 = -1;
        private int Sel0 {
            get {
                return _sel0;
            }
            set {
                if (value < _sel0) {
                    if (_sel0 <= 0) {
                        _sel0 = chars.Length - 1;
                    }
                    else {
                        _sel0 = value;
                    }

                    if (_sel0 == Sel1) {
                        Sel0--;
                    }
                }
                else if (value > _sel0) {
                    if (_sel0 >= chars.Length - 1) {
                        _sel0 = 0;
                    }
                    else {
                        _sel0 = value;
                    }

                    if (_sel0 == Sel1) {
                        Sel0++;
                    }
                }
            }
        }

        private int _sel1 = -1;
        private int Sel1 {
            get {
                return _sel1;
            }
            set {
                if (value < _sel1) {
                    if (_sel1 <= 0) {
                        _sel1 = chars.Length - 1;
                    }
                    else {
                        _sel1 = value;
                    }

                    if (_sel1 == Sel0) {
                        Sel1--;
                    }
                }
                else if (value > _sel1) {
                    if (_sel1 >= chars.Length - 1) {
                        _sel1 = 0;
                    }
                    else {
                        _sel1 = value;
                    }

                    if (_sel1 == Sel0) {
                        Sel1++;
                    }
                }
            }
        }

        void Start() {
            Cursor.visible = false;
            gridChars = new GameObject[chars.Length];
            for (int i = 0; i < chars.Length; i++) {
                gridChars[i] = Instantiate(chars[i].charDisplay, grid.transform);
            }
        }

        void Update() {
            if (Selection.GetPlayer0() == -1 && Input.GetButtonDown("p0_horizontal")) {
                if (Sel0 != -1) {
                    gridChars[Sel0].GetComponent<GridChar>().SetHovered(-1);
                }

                if (Input.GetAxisRaw("p0_horizontal") < 0) {
                    Sel0--;
                }
                else if (Input.GetAxisRaw("p0_horizontal") > 0) {
                    Sel0++;
                }

                if (Sel0 != -1) {
                    gridChars[Sel0].GetComponent<GridChar>().SetHovered(0);
                }
            }
            if (Selection.GetPlayer1() == -1 && Input.GetButtonDown("p1_horizontal")) {
                if (Sel1 != -1) {
                    gridChars[Sel1].GetComponent<GridChar>().SetHovered(-1);
                }

                if (Input.GetAxisRaw("p1_horizontal") < 0) {
                    Sel1--;
                }
                else if (Input.GetAxisRaw("p1_horizontal") > 0) {
                    Sel1++;
                }

                if (Sel1 != -1) {
                    gridChars[Sel1].GetComponent<GridChar>().SetHovered(1);
                }
            }

            if (Selection.GetPlayer0() != -1 && Input.GetButtonDown("p0_attack")) {
                gridChars[Sel0].GetComponent<GridChar>().SetSelected(-1);
                gridChars[Sel0].GetComponent<GridChar>().SetHovered(0);
                Selection.SetPlayer0(-1);
            }
            else if (Sel0 != -1 && Input.GetButtonDown("p0_jump")) {
                gridChars[Sel0].GetComponent<GridChar>().SetHovered(-1);
                gridChars[Sel0].GetComponent<GridChar>().SetSelected(0);
                Selection.SetPlayer0(chars[Sel0].id);
            }

            if (Selection.GetPlayer1() != -1 && Input.GetButtonDown("p1_attack")) {
                gridChars[Sel1].GetComponent<GridChar>().SetSelected(-1);
                gridChars[Sel1].GetComponent<GridChar>().SetHovered(1);
                Selection.SetPlayer1(-1);
            }
            else if (Sel0 != -1 && Input.GetButtonDown("p1_jump")) {
                gridChars[Sel1].GetComponent<GridChar>().SetHovered(-1);
                gridChars[Sel1].GetComponent<GridChar>().SetSelected(1);
                Selection.SetPlayer1(chars[Sel1].id);
            }

        }
    }
}